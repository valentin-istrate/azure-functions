using System.Collections.Generic;

namespace SalesAnalizerDurableFunction.Models
{
    public class ProfitReport
    {
        public string Region { get; set; }

        public IReadOnlyList<CountryProfit> CountriesProfits { get; set; }
    }
}
