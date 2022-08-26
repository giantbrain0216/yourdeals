// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Circle.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models
{
    /// <summary>
    /// The circle.
    /// </summary>
    public class Circle : Path
    {
        /// <summary>
        ///     Center of the circle.
        /// </summary>
        public LatLng Position { get; set; }

        /// <summary>
        ///     Radius of the circle, in meters.
        /// </summary>
        public float Radius { get; set; }
    }
}