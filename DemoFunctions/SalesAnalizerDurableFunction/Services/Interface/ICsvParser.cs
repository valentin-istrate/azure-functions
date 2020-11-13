
namespace SalesAnalizerDurableFunction.Services.Interface
{
    using SalesAnalizerDurableFunction.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICsvParser
    {
        public Task<IReadOnlyList<SaleInfo>> ParseSaleData(string saleData);
    }
}
