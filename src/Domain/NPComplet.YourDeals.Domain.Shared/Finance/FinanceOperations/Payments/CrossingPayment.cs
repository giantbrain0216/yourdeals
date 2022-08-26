// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealPayment.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments
{
    using NPComplet.YourDeals.Domain.Shared.Enumerable;
    using System;
    #region

    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    /// <summary>
    /// </summary>
    [Table("CROSSINGPAYMENTS", Schema = "FINANCE")]
    public class CrossingPayment : FinanceOperation
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid? DealId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Deal.Deal Deal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DealType DealType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid? PropositionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Deal.Proposition  Proposition { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsPaid { get; set; }
    }
}