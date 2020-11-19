namespace Demo.SalesAnalyzerDurableFunction.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class SaleAnalysisResultEntity
    {
        [JsonProperty("reports")] 
        public ProfitReport[] Reports { get; set; }

        public void Store(ProfitReport[] reports)
        {
            Reports = reports;
        }


        public IReadOnlyList<ProfitReport> Get()
        {
            return Reports;
        }
    }
}