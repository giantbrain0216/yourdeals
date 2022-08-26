using System.ComponentModel.DataAnnotations.Schema;
using NPComplet.YourDeals.Domain.Shared.Enumerable;
using NPComplet.YourDeals.Domain.Shared.Pricing;

namespace NPComplet.YourDeals.Domain.Shared.Finance.FinanceSettings.Payout
{
    /// <summary>
    /// 
    /// </summary>
   public class PayoutFeesTier:FeesTire
    {
        /// <summary>
        /// 
        /// </summary>
        public FinanceSupportName FinanceSupportName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(15,2)")]
        public Amount Amount { get; set; }
    }
}
