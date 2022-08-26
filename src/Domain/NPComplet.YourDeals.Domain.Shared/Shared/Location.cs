// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Location.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Shared
{
    #region

    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;

    #endregion

    /// <summary>
    ///     Describe the location
    /// </summary>
    [Table("LOCATIONS", Schema = "SHARED")]
    public class Location : BaseEntityDomain
    {
        /// <summary>
        ///     the country code
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        ///     the X location
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Radius in meters
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        ///     the Y location
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        ///     State Code
        /// </summary>
        public string StateCode { get; set; }
    }
}