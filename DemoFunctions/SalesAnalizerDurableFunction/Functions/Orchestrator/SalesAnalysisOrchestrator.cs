namespace Demo.SalesAnalyzerDurableFunction.Functions.Orchestrator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Activity;
    using Aggregator;
    using Constants;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Extensions.Logging;
    using Models;

    public class SalesAnalyzerOrchestrator
    {
        private readonly int userAssessmentTimeout;
        private readonly ILogger<SalesAnalyzerOrchestrator> log;

        public SalesAnalyzerOrchestrator(ILogger<SalesAnalyzerOrchestrator> log)
        {
            this.log = log;

            string? configuredUserAssessmentTimeout =
                Environment.GetEnvironmentVariable(FunctionConstants.UserAssessmentDurationInHours);
            if (!int.TryParse(configuredUserAssessmentTimeout, out this.userAssessmentTimeout))
            {
                this.userAssessmentTimeout = 24;
                log.LogWarning(
                    $"{FunctionConstants.UserAssessmentDurationInHours} was not set. Using default value of '{this.userAssessmentTimeout}");
            }
        }

        [FunctionName(nameof(SalesAnalyzerOrchestrator))]
        public async Task<object> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            log.LogInformation($"Running sale analysis orchestration. Orchestration replaying: {context.IsReplaying}");

            ProfitReport[] reports = await ExecuteSaleAnalysis(context);
            StoreAnalysisResult(context, reports);
            object result = await AwaitUserAssessment(context, reports);

            log.LogInformation("Sale analysis orchestration completed.");
            context.SetCustomStatus("Completed");
            return result;
        }

        private async Task<ProfitReport[]> ExecuteSaleAnalysis(IDurableOrchestrationContext context)
        {
            context.SetCustomStatus("DataAnalysis");

            var inputData = context.GetInput<IList<RegionData>>();
            if (!context.IsReplaying)
            {
                log.LogInformation($"Running {inputData.Count} sale data analysis functions...");
            }

            IList<Task<ProfitReport>> analysisTasks = new List<Task<ProfitReport>>();
            foreach (RegionData regionData in inputData)
            {
                analysisTasks.Add(context.CallActivityAsync<ProfitReport>(nameof(AnalyzeSalesActivity), regionData));
            }

            ProfitReport[] reports = await Task.WhenAll(analysisTasks);

            log.LogInformation($"Data analysis execution completed.");
            return reports;
        }

        private void StoreAnalysisResult(IDurableOrchestrationContext context, ProfitReport[] reports)
        {
            context.SetCustomStatus("StoreData");
            log.LogInformation("Storing data to durable entity...");

            var entityId = new EntityId(nameof(SaleAnalysisResultsAggregator), context.InstanceId);
            context.SignalEntity(entityId, nameof(SaleAnalysisResultEntity.Store), reports);
        }

        private async Task<object> AwaitUserAssessment(IDurableOrchestrationContext context, ProfitReport[] reports)
        {
            context.SetCustomStatus("AwaitingApproval");
            log.LogInformation("Awaiting user assessment...");

            using var timeoutCts = new CancellationTokenSource();
            Task durableTimeout = CreateTimeoutResolutionTimer(context, timeoutCts);

            Task<bool> approvalEvent = context.WaitForExternalEvent<bool>(FunctionConstants.ApprovalEvent);

            object result;
            Task resolution = await Task.WhenAny(approvalEvent, durableTimeout);
            if (resolution == durableTimeout)
            {
                log.LogInformation("Timeout expired, data was not approved.");
                result = "Data was not approved in the configured amount of time.";
            }
            else
            {
                timeoutCts.Cancel();
                if (await approvalEvent)
                {
                    log.LogInformation("Data analysis was approved.");
                    result = reports;
                }
                else
                {
                    log.LogInformation("Data analysis was rejected.");
                    result = "Report data was rejected.";
                }
            }

            return result;
        }

        private Task CreateTimeoutResolutionTimer(IDurableOrchestrationContext context,
            CancellationTokenSource timeoutCts)
        {
            DateTime dueTime = context.CurrentUtcDateTime.AddHours(userAssessmentTimeout);
            Task durableTimeout = context.CreateTimer(dueTime, timeoutCts.Token);
            return durableTimeout;
        }
    }
}