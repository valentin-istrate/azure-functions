
namespace Demo.SalesAnalyzerDurableFunction.Services
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using CsvHelper;
    using Interface;
    using Microsoft.Extensions.Logging;
    using Models;

    public class CsvParser : ICsvParser
    {
        private readonly ILogger logger;

        public CsvParser(ILogger<CsvParser> logger)
        {
            this.logger = logger;
        }

        public async Task<IReadOnlyList<SaleInfo>> ParseSaleData(string saleDataCsv)
        {
            logger.LogDebug("Parsing data...");
            IReadOnlyList<SaleInfo> saleInfos;

            using (var salesStringCsv = new StringReader(saleDataCsv))
            {
                using (var reader = new CsvReader(salesStringCsv, CultureInfo.InvariantCulture))
                {
                    saleInfos = await reader.GetRecordsAsync<SaleInfo>().ToListAsync();
                }
            }
            logger.LogDebug("Done parsing data.");
            return saleInfos;
        }
    }
}
