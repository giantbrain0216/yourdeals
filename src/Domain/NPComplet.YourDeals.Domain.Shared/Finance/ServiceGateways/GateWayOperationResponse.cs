using NPComplet.YourDeals.Domain.Shared.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways
{
    /// <summary>
    /// 
    /// </summary>
    [Table("GateWayOperationResponses", Schema = "FINANCE")]
    public class GateWayOperationResponse : BaseEntityDomain
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid FiniancialOpreationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FinanceOperation FinanceOperation { get; set; }
    }
}
