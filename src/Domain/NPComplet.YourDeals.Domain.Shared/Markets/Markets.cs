// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Markets.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Markets
{
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;

    #endregion

    /// <summary>
    /// </summary>
    [Table("COLLECTION_MARKETS", Schema = "MARKET")]
    public class Markets : BaseEntityDomain
    {
        /// <summary>
        ///     the market list
        /// </summary>
        public ICollection<Market> MarketsList { get; set; }
    }
}