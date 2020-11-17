
namespace Demo.SalesAnalyzerDurableFunction.Models
{
    using System;
    using CsvHelper.Configuration.Attributes;

    public class SaleInfo
    {
        public string Country { get; set; }

        [Name("Item Type")]
        public string ItemType { get; set; }

        [Name("Sales Channel")]
        public string SalesChannel { get; set; }

        [Name("Order Priority")]
        public string OrderPriority { get; set; }

        [Name("Order Date")]
        public string OrderDate { get; set; }

        [Name("Order ID")]
        public string OrderId { get; set; }

        [Name("Ship Date")]
        public DateTime ShipDate { get; set; }

        [Name("Units Sold")]
        public int UnitsSold { get; set; }
        [Name("Unit Price")]
        public double UnitPrice { get; set; }

        [Name("Unit Cost")]
        public double UnitCost { get; set; }

        [Name("Total Revenue")]
        public double TotalRevenue { get; set; }

        [Name("Total Cost")]
        public double TotalCost { get; set; }

        [Name("Total Profit")]
        public double TotalProfit { get; set; }
    }
}
