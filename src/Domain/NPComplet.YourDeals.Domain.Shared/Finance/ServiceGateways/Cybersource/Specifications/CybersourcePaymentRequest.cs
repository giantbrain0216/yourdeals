using NPComplet.YourDeals.Domain.Shared.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;


using NPComplet.YourDeals.Domain.Shared.Enumerable;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceSupports;

namespace NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Cybersource.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    [Table("CYBERSORUCEPAYMENTSREQUEST", Schema = "FINANCE")]
    public class CybersourcePaymentRequest  : GateWayOperationRequest
    {

        /// <summary>
        ///  json daa request
        /// </summary>
        public bool CaptureTrueForProcessPayment { get; set; }
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
        /// 
        /// </summary>
        public CallState CallState { get; set; }
    }
}
