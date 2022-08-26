using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NPComplet.YourDeals.Domain.Shared.Enumerable;

namespace NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Cybersource.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    [Table("CYBERSORUCEPAYOUTS", Schema = "FINANCE")]
    public class CybersourcePayout : GateWayOperationPayout
    {
        /// <summary>
        /// 
        /// </summary>
        public FinanceOperationType FinanceOperationType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? FinanceOperationId { get; set; }

        /// <summary>
        ///  can be DealPayment , or Posting DelPayment
        /// </summary>
        public FinanceOperation FinanceOperation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<CybersourcePayoutRequest> CybersourcePayoutsRequests { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public ICollection<CybersourcePayoutResponse> CybersourcePayoutsResponses { get; set; }
        /// <summary>
        /// 
        /// </summary>


        public FinanceGateWayOperationState FinanceGateWayOperationState { get; set; }

    }
}
