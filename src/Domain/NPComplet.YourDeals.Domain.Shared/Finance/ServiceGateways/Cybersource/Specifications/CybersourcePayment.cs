using NPComplet.YourDeals.Domain.Shared.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using NPComplet.YourDeals.Domain.Shared.Enumerable;
using NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Stripe;

namespace NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Cybersource.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    [Table("CYBERSORUCEPAYMENTS", Schema = "FINANCE")]
    public class CybersourcePayment : GateWayOperationPayment
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
        public ICollection<CybersourcePaymentRequest> CybersourcePaymentsRequests { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public ICollection<CybersourcePaymentResponse> CybersourcePaymentsResponses { get; set; }


        public FinanceGateWayOperationState FinanceGateWayOperationState { get; set; }


    }
}
