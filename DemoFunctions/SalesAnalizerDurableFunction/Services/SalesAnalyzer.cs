
namespace Demo.SalesAnalyzerDurableFunction.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using Interface;
    using Models;
    using MoreLinq;

    public class SalesAnalyzer : ISalesAnalyzer
    {
        public IReadOnlyList<CountryProfit> AnalyzeProfits(IReadOnlyList<SaleInfo> saleInfos)
        {

            IEnumerable<IGrouping<string, SaleInfo>> countiesData = saleInfos.GroupBy(saleInfo => saleInfo.Country);
            IEnumerable<CountryProfit> countryProfits = countiesData.Select(countryData => new CountryProfit
            {
                Country = countryData.Key,
                OnlineProfit = new ProfitInfo
                {
                    Profit = this.GetProfitForChannel(countryData, SalesChannelType.Online),
                    HighestProfitCategory = this.GetHighestProfitItem(countryData, SalesChannelType.Online)
                },
                OfflineProfit = new ProfitInfo
                {
                    Profit = this.GetProfitForChannel(countryData, SalesChannelType.Offline),
                    HighestProfitCategory = this.GetHighestProfitItem(countryData, SalesChannelType.Offline)
                }
            });

            return countryProfits.ToList();
        }

        private double GetProfitForChannel(IGrouping<string, SaleInfo> countryData, string salesChannel)
        {
            return countryData.Where(countrySales => countrySales.SalesChannel == salesChannel)
                .Select(countrySales => countrySales.TotalProfit)
                .Sum();
        }

        private string GetHighestProfitItem(IGrouping<string, SaleInfo> countryData, string salesChannel)
        {
            return countryData
                .Where(countrySales => countrySales.SalesChannel == salesChannel)
                .GroupBy(countrySales => countrySales.ItemType)
                .Select(itemSales => new { Item = itemSales.Key, Profit = itemSales.Select(itemSale => itemSale.TotalProfit).Sum() })
                .MaxBy(sale => sale.Profit)
                .First()
                .Item;
        }
    }
}
