using NPComplet.YourDeals.Domain.Shared.Base;
using NPComplet.YourDeals.Domain.Shared.Enumerable;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Cybersource.Specifications
{
    /// <summary>
    /// 
    /// </summary>
    [Table("CYBERSORUCEPAYMENTSRESPONSE", Schema = "FINANCE")]
    public class CybersourcePaymentResponse : GateWayOperationResponse
    {
        /// <summary>
        /// json response
        /// </summary>
        public CallState CallState { get; set; }
    }
}
