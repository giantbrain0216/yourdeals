using NPComplet.YourDeals.Domain.Shared.Pricing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.Finance.FinanceSettings.PostDeal
{
    /// <summary>
    /// 
    /// </summary>
    [Table("POSTDEALFEESTIERS", Schema = "FINNACE")]
    public class PostDealFeesTier:FeesTire
    {
        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(15,2)")]
        public Amount Amount { get; set; }
    }
}
