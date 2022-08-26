
using System;

namespace NPComplet.YourDeals.Domain.Shared.Finance
{
    using NPComplet.YourDeals.Domain.Shared.Base;
    using NPComplet.YourDeals.Domain.Shared.Enumerable;
    using NPComplet.YourDeals.Domain.Shared.Pricing;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 
    /// </summary>
    [Table("FINANCEOPERATIONS", Schema = "FINANCE")]
    public class FinanceOperation : BaseEntityDomain
    {
        /// <summary>
        ///  Finance operation type 
        /// </summary>
        public FinanceOperationType FinanceOperationType { get; set; }

        /// <summary>
        ///     The amount indentifier.
        /// </summary>
        public Guid? AmountId { get; set; }

        /// <summary>
        ///     the amount of the operation
        /// </summary>
        public Amount Amount { get; set; }

        /// <summary>
        ///  the state of the operation 
        /// </summary>
        public FinanceOperationState FinanceOperationState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? ServiceGatewayId { get; set; }
        /// <summary>
        /// The gateway which make the operation
        /// </summary>
        public ServiceGateway ServiceGateway { get; set; }

        /// <summary>
        /// Service Gatewayname
        /// </summary>
        public ServiceGatewayName ServiceGatewayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? FinanceSupportId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FinanceSupport FinanceSupport { get; set; }

        /// <summary>
        /// Fianance SuportName
        /// </summary>
        public FinanceSupportName FinanceSupportName { get; set; }

        /// <summary>
        ///  Parent finance operation in case of mile stone payment  
        /// </summary>
        public Guid? ParentFinanceOperationId { get; set; }

        /// <summary>
        ///  finance operation type in the case of the milestone payment for example 
        /// </summary>
        public FinanceOperation? ParentFinanceOperation { get; set; }
    }
}
