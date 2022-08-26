// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LatLng.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models
{
    #region

    using System.Drawing;

    #endregion

    /// <summary>
    /// The lat lng.
    /// </summary>
    public class LatLng
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LatLng"/> class.
        /// </summary>
        public LatLng()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LatLng"/> class.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        public LatLng(PointF position)
            : this(position.X, position.Y)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LatLng"/> class.
        /// </summary>
        /// <param name="lat">
        /// The lat.
        /// </param>
        /// <param name="lng">
        /// The lng.
        /// </param>
        public LatLng(float lat, float lng)
        {
            this.Lat = lat;
            this.Lng = lng;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LatLng"/> class.
        /// </summary>
        /// <param name="lat">
        /// The lat.
        /// </param>
        /// <param name="lng">
        /// The lng.
        /// </param>
        /// <param name="alt">
        /// The alt.
        /// </param>
        public LatLng(float lat, float lng, float alt)
            : this(lat, lng)
        {
            this.Alt = alt;
        }

        /// <summary>
        /// Gets or sets the alt.
        /// </summary>
        public float Alt { get; set; }

        /// <summary>
        /// Gets or sets the lat.
        /// </summary>
        public float Lat { get; set; }

        /// <summary>
        /// Gets or sets the lng.
        /// </summary>
        public float Lng { get; set; }

        /// <summary>
        /// The to point f.
        /// </summary>
        /// <returns>
        /// The <see cref="PointF"/>.
        /// </returns>
        public PointF ToPointF()
        {
            return new PointF(this.Lat, this.Lng);
        }
    }
}