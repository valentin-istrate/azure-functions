namespace Demo.SalesAnalyzerDurableFunction.Functions.Endpoints
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.Extensions.Logging;
    using Models;
    using Orchestrator;

    public class StartSaleAnalysis
    {
        private readonly ILogger<StartSaleAnalysis> log;

        public StartSaleAnalysis(ILogger<StartSaleAnalysis> log)
        {
            this.log = log;
        }

        [FunctionName(nameof(StartSaleAnalysis))]
        public async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")]
            HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter)
        {
            List<RegionData> inputData = await ReadAllRegionData(req);

            string instanceId = await starter.StartNewAsync(nameof(SalesAnalyzerOrchestrator), inputData);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }

        private static async Task<List<RegionData>> ReadAllRegionData(HttpRequestMessage req)
        {
            MultipartMemoryStreamProvider dataFiles = await GetMultipartStream(req);
            var inputData = new List<RegionData>();
            foreach (HttpContent dataContent in dataFiles.Contents)
            {
                var regionData = new RegionData
                {
                    DataCsv = await dataContent.ReadAsStringAsync(),
                    FileName = dataContent.Headers.ContentDisposition.FileName
                };
                inputData.Add(regionData);
            }

            return inputData;
        }

        private static async Task<MultipartMemoryStreamProvider> GetMultipartStream(HttpRequestMessage req)
        {
            if (!req.Content.IsMimeMultipartContent())
            {
                throw new InvalidDataException("Expected multipart content.");
            }

            MultipartMemoryStreamProvider dataFiles = await req.Content.ReadAsMultipartAsync();
            return dataFiles;
        }
    }
}