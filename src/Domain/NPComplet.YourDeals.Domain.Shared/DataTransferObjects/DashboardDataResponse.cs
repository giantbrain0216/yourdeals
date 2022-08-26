using System;
using System.Collections.Generic;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class DashboardDataResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public int OfferCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int RequestCount { get; set; }
       
        public int UserCount { get; set; }
        public int RoleCount { get; set; }
        public List<ChartSeries> DataEnterBarChart { get; set; } = new();
        public Dictionary<string, double> DealsByBrandTimePieChart { get; set; }
    }

    public class ChartSeries
    {
        public ChartSeries() { }

        public string Name { get; set; }
        public double[] Data { get; set; }
    }
}
