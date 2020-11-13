namespace SalesAnalizerDurableFunction.Services.Interface
{
    using SalesAnalizerDurableFunction.Models;
    using System.Collections.Generic;

    public interface ISalesAnalyzer
    {
        public IReadOnlyList<CountryProfit> AnalyzeProfits(IReadOnlyList<SaleInfo> saleInfos);
    }
}
