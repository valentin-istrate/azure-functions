namespace Demo.SalesAnalyzerDurableFunction.Models
{
    using System.Collections.Generic;

    public class ProfitReport
    {
        public string FileName { get; set; }

        public IReadOnlyList<CountryProfit> CountriesProfits { get; set; }
    }
}
