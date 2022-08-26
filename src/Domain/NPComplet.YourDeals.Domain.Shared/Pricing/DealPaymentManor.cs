using NPComplet.YourDeals.Domain.Shared.Base;
using NPComplet.YourDeals.Domain.Shared.Enumerable;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPComplet.YourDeals.Domain.Shared.Pricing
{
    /// <summary>
    /// The deal payment manor.
    /// </summary>
    [Table("DEALPAYMENTMANORS", Schema = "PRICING")]
    public class DealPaymentManor : BaseEntityDomain
    {
        /// <summary>
        /// The payment manor.
        /// </summary>
        public PaymentManor PaymentManor { get; set; }
    }
}
