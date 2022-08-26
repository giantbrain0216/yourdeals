using NPComplet.YourDeals.Domain.Shared.Base;
using NPComplet.YourDeals.Domain.Shared.Deal;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.CashIn;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NPComplet.YourDeals.Domain.Shared.Finance
{
    /// <summary>
    /// 
    /// </summary>
    [Table("PROPOSITIONSFINANCEOPERATIONS", Schema = "FINANCE")]
    public class PropositionsFinanceOperation : BaseEntityDomain
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual Proposition Propsosation { get; set; }
        /// <summary>
        /// The crossing payment finance operations.
        /// </summary>
        public ICollection<CrossingPayment> CrossingPaymentFinanceOperations { get; set; }

        /// <summary>
        /// The crossing payout finance operations.
        /// </summary>
        public ICollection<CrossingPayout> CrossingPayoutFinanceOperations { get; set; }
    }
}
