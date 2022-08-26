using NPComplet.YourDeals.Domain.Shared.Base;
using NPComplet.YourDeals.Domain.Shared.Enumerable;
using NPComplet.YourDeals.Domain.Shared.Pricing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.Finance.EarningsWallet
{
    /// <summary>
    /// 
    /// </summary>
    [Table("NPCOMPLETEARNINGSWALLET", Schema = "FINANCE")]
    public class NPCompletEarningsWallet : BaseEntityDomain
    {

        /// <summary>
        /// 
        /// </summary>
        public Guid? FinanceOperationId { get; set; }

        /// <summary>
        ///  can be DealPayment , or Posting DeelPayment
        /// </summary>
        public FinanceOperation FinanceOperation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CashFlow CashFlow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(15,2)")]
        public Amount Amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column(TypeName = "decimal(15,2)")]
        public decimal AvailableBlance { get; set; }
    }
}
