// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Market.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Markets
{
    #region

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using System.Text.Json.Serialization;
    using NPComplet.YourDeals.Domain.Shared.Base;
    using NPComplet.YourDeals.Domain.Shared.Deal;

    #endregion

    /// <summary>
    ///     the market namespace
    /// </summary>
    [Table("MARKETS", Schema = "MARKET")]
    public class Market : BaseEntityDomain
    {
        /// <summary>
        ///     All Deals
        /// </summary>
        public ICollection<Deal> Deals { get; set; }

        /// <summary>
        ///     the market end time
        /// </summary>
        public DateTime? MarketEndTimeUtc { get; set; }

        /// <summary>
        /// </summary>
        [IgnoreDataMember]
        [JsonIgnore]
        public virtual Markets Markets { get; set; }

        /// <summary>
        /// </summary>
        public Guid? MarketsId { get; set; }

        /// <summary>
        ///     the market start time
        /// </summary>
        public DateTime MarketStartTimeUtc { get; set; }

        /// <summary>
        ///     the opening time span
        /// </summary>
        public TimeSpan OpeningTimeSpan { get; set; }
    }
}