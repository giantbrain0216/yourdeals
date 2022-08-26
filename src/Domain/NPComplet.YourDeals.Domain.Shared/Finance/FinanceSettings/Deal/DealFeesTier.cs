using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.Finance.FinanceSettings.Deal
{
    /// <summary>
    /// 
    /// </summary>
    [Table("DEALFEESTIERS", Schema = "FINNACE")]
    public class DealFeesTier :FeesTire
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(15,2)")]
        public decimal MoneyLimit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(5,2)")]
        public decimal Percentage { get; set; }
    }
}
