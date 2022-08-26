// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitPricing.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Pricing
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;
    using NPComplet.YourDeals.Domain.Shared.Enumerable;

    #endregion

    /// <summary>
    ///     the payment unit
    /// </summary>
    [Table("UNITPRICINGS", Schema = "PRICING")]
    public class UnitPricing : BaseEntityDomain
    {
        /// <summary>
        ///     Currency
        /// </summary>
        public Currency CurrencyCode { get; set; }

        /// <summary>
        ///     Currency code
        /// </summary>
        public Guid? CurrencyCodeId { get; set; }

        /// <summary>
        ///     Taxes
        /// </summary>
        [Column(TypeName = "decimal(15,2)")]
        public decimal Taxes { get; set; }

        /// <summary>
        ///     the Unit source type
        /// </summary>
        public UnitSourceType Type { get; set; }
    }
}