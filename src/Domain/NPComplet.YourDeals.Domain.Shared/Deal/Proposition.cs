// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Proposition.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using NPComplet.YourDeals.Domain.Shared.Finance;

namespace NPComplet.YourDeals.Domain.Shared.Deal
{
    #region
    using Base;
    using Communication;
    using Enumerable;
    using Pricing;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    /// <summary>
    /// </summary>
    [Table("PROPOSITIONS", Schema = "DEAL")]
    public class Proposition : BaseEntityDomain
    {
        /// <summary>
        ///     Define is the offer is selected
        /// </summary>
        public bool? IsSelected { get; set; }

        /// <summary>
        ///     public  proposition amount
        /// </summary>
        public Amount PropositionAmount { get; set; }

        /// <summary>
        /// The payment manor.
        /// </summary>
        public PaymentManor? PaymentManor { get; set; }

        /// <summary>
        /// The proposition messages post.
        /// </summary>
        public PropositionMessagesPost PropositionMessagesPost { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? PropositionsFinanceOperationId { get; set; }
        /// <summary>
        /// The proposition finance operation.
        /// </summary>
        public PropositionsFinanceOperation PropositionsFinanceOperation { get; set; }
    }
}