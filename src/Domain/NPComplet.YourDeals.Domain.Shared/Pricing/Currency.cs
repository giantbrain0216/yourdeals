// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Currency.cs" company="NPComplet">
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
    [Table("CURRENCIES", Schema = "PRICING")]
    public class Currency : BaseEntityDomain
    {
        /// <summary>
        /// </summary>
        public string Code { get; set; }
    }
}