namespace Demo.SalesAnalyzerDurableFunction.Functions.Aggregator
{
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Extensions.Logging;
    using Models;

    public class SaleAnalysisResultsAggregator
    {
        private readonly ILogger<SaleAnalysisResultsAggregator> log;

        public SaleAnalysisResultsAggregator(
            ILogger<SaleAnalysisResultsAggregator> log)
        {
            this.log = log;
        }

        [FunctionName(nameof(SaleAnalysisResultsAggregator))]
        public Task Run([EntityTrigger] IDurableEntityContext ctx)
        {
            log.LogDebug($"Executing operation '{ctx.OperationName}' on entity '{ctx.EntityId}'.");

            return ctx.DispatchAsync<SaleAnalysisResultEntity>();
        }
    }
}