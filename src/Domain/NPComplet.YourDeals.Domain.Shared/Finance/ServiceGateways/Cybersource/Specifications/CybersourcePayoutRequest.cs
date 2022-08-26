
using NPComplet.YourDeals.Domain.Shared.Base;
using NPComplet.YourDeals.Domain.Shared.Enumerable;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceSupports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Cybersource.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    [Table("CYBERSORUCEPAYOUTSREQUEST", Schema = "FINANCE")]
    public class CybersourcePayoutRequest : GateWayOperationRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid? CreditCardId { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public CreditCard CreditCard { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? BankAccountId { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public BankAccount BankAccount { get; set; }

        /// <summary>
        /// json response
        /// </summary>
        public CallState CallState { get; set; }
    }
}
