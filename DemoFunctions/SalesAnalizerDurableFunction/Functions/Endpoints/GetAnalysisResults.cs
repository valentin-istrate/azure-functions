namespace Demo.SalesAnalyzerDurableFunction.Functions.Endpoints
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Aggregator;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;

    public class GetAnalysisResults
    {
        private readonly ILogger<GetAnalysisResults> log;

        public GetAnalysisResults(ILogger<GetAnalysisResults> log)
        {
            this.log = log;
        }

        [FunctionName(nameof(GetAnalysisResults))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAnalysisResults/{instanceId}")]
            HttpRequestMessage req,
            string instanceId,
            [DurableClient] IDurableEntityClient entityClient)
        {
            log.LogDebug($"Requesting sale analysis results for instance id: '{instanceId}'");

            var entityId = new EntityId(nameof(SaleAnalysisResultsAggregator), instanceId);

            EntityStateResponse<JObject> stateResponse =
                await entityClient.ReadEntityStateAsync<JObject>(entityId);

            return new OkObjectResult(stateResponse);
        }
    }
}