// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapResponse.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects
{
    /// <summary>
    ///     The map response.
    /// </summary>
    public class MapResponse
    {
        /// <summary>
        ///     Gets or set the display name value.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets or set the lat value.
        /// </summary>
        public float Lat { get; set; }

        /// <summary>
        ///     Gets or set the lon value.
        /// </summary>
        public float Lon { get; set; }

        /// <summary>
        /// radius in meters
        /// </summary>
        public float radius { get; set; }
    }
}