namespace Demo.SalesAnalyzerDurableFunction.Services.Interface
{
    using System.Collections.Generic;
    using Models;

    public interface ISalesAnalyzer
    {
        public IReadOnlyList<CountryProfit> AnalyzeProfits(IReadOnlyList<SaleInfo> saleInfos);
    }
}
