
namespace Demo.SalesAnalyzerDurableFunction.Services.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    public interface ICsvParser
    {
        public Task<IReadOnlyList<SaleInfo>> ParseSaleData(string saleData);
    }
}
