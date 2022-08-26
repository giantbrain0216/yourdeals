using NPComplet.YourDeals.Domain.Shared.Base;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.CashIn;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.Finance
{
    /// <summary>
    /// 
    /// </summary>
    [Table("DEALSFINANCEOPERATIONS", Schema = "FINANCE")]
    public class DealsFinanceOpertation : BaseEntityDomain
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual Deal.Deal Deal { get; set; }
        /// <summary>
        /// all DealPayment FinanceOperations
        /// </summary>
        public ICollection<CrossingPayment> DealPaymentFinanceOperations { get; set; }

        /// <summary>
        /// all DealPayout FinanceOperations
        /// </summary>
        public ICollection<CrossingPayout> DealPayoutFinanceOperations { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PostingDealPayment PostingDealPayment { get; set; }
    }
}
