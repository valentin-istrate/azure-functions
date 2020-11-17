namespace Demo.SalesAnalyzerDurableFunction.Models
{
    public class CountryProfit
    {
        public string Country { get; set; }

        public ProfitInfo OnlineProfit { get; set; }

        public ProfitInfo OfflineProfit { get; set; }
    }
}
