// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealPriceReference.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Pricing
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;


    /// <summary>
    /// </summary>
    [Table("DEALPRICEREFERENCES", Schema = "PRICING")]
    public class DealPriceReference : Amount
    {
        /// <summary>
        ///     Payment Manor for the deal Price
        /// </summary>
        public ICollection<DealPaymentManor> PaymentManors { get; set; }
    }
}