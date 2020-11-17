namespace Demo.SalesAnalyzerDurableFunction.Functions.Endpoints
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Constants;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;

    public class AssessSalesAnalysis
    {
        private readonly ILogger<GetAnalysisResults> log;

        public AssessSalesAnalysis(ILogger<GetAnalysisResults> log)
        {
            this.log = log;
        }

        [FunctionName(nameof(AssessSalesAnalysis))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",
                Route = "AssessSalesAnalysis/{instanceId}/{approved:bool}")]
            HttpRequestMessage req,
            string instanceId,
            bool approved,
            [DurableClient] IDurableOrchestrationClient orchestrationClient)
        {
            log.LogDebug($"Requesting results for instance id: '{instanceId}'");

            await orchestrationClient.RaiseEventAsync(instanceId, FunctionConstants.ApprovalEvent, approved);

            return new OkObjectResult(GetAssessmentMessage(approved));
        }

        private string GetAssessmentMessage(bool approved)
        {
            string message = approved
                ? "The sale analysis has been approved."
                : "The sale analysis has been rejected.";

            return message;
        }
    }
}