// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Amount.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Pricing
{
    #region

    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;

    #endregion

    /// <summary>
    /// </summary>
    [Table("AMOUNTS", Schema = "PRICING")]
    public class Amount : BaseEntityDomain
    {
        /// <summary>
        ///     the price of the deal
        /// </summary>
        public UnitPricing AmountUnit { get; set; }

        /// <summary>
        ///     the Amount of a unit
        /// </summary>
        [Column(TypeName = "decimal(15,2)")]
        public decimal AmountValue { get; set; }
    }
}