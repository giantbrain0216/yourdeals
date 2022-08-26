// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PostingDealPayment.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments
{
    using System;
    using System.Collections.Generic;
    using NPComplet.YourDeals.Domain.Shared.Deal;
    #region

    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    /// <summary>
    ///     The posting deal payment class.
    /// </summary>
    [Table("POSTINGDEALPAYMENTS", Schema = "FINNACE")]
    public class PostingDealPayment : FinanceOperation
    {
        /// <summary>
        /// 
        /// </summary>
      public Guid DealId { get; set; }
        /// <summary>
        /// 
        /// </summary>
      public Deal Deal { get; set; }

       
    }
}