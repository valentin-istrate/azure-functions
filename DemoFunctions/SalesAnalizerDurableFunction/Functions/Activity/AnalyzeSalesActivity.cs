namespace Demo.SalesAnalyzerDurableFunction.Functions.Activity
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.DurableTask;
    using Microsoft.Extensions.Logging;
    using Models;
    using Services.Interface;

    public class AnalyzeSalesActivity
    {
        private readonly ICsvParser csvParser;
        private readonly ISalesAnalyzer salesAnalyzer;
        private readonly ILogger<AnalyzeSalesActivity> logger;

        public AnalyzeSalesActivity(
            ICsvParser csvParser,
            ISalesAnalyzer salesAnalyzer,
            ILogger<AnalyzeSalesActivity> logger)
        {
            this.csvParser = csvParser;
            this.salesAnalyzer = salesAnalyzer;
            this.logger = logger;
        }

        [FunctionName(nameof(AnalyzeSalesActivity))]
        public async Task<ProfitReport> AnalyzeSales([ActivityTrigger] RegionData regionData)
        {
            logger.LogDebug($"Running sale analysis on {regionData.FileName}");

            IReadOnlyList<SaleInfo> saleInfos = await csvParser.ParseSaleData(regionData.DataCsv);

            var report =  new ProfitReport
            {
                FileName = regionData.FileName,
                CountriesProfits = salesAnalyzer.AnalyzeProfits(saleInfos)
            };

            logger.LogDebug($"Analysis complete for region {regionData.FileName}.");
            return report;
        }
    }
}