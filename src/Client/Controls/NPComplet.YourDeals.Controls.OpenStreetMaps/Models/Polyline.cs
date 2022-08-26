// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Polyline.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models
{
    #region

    using System.Drawing;

    #endregion

    /// <summary>
    /// The polyline.
    /// </summary>
    /// <typeparam name="TShape">
    /// </typeparam>
    public class Polyline<TShape> : Path
    {
        /// <summary>
        ///     Disable polyline clipping.
        /// </summary>
        public bool NoClipEnabled { get; set; }

        /// <summary>
        /// Gets or sets the shape.
        /// </summary>
        public TShape Shape { get; set; }

        /// <summary>
        ///     How much to simplify the polyline on each zoom level. More means better performance and smoother look, and less
        ///     means more accurate representation.
        /// </summary>
        public double SmoothFactory { get; set; } = 1.0;
    }

    /// <summary>
    /// The polyline.
    /// </summary>
    public class Polyline : Polyline<PointF[][]>
    {
    }
}