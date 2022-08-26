// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MouseEvent.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models.Events
{
    #region

    using System.Drawing;

    #endregion

    /// <summary>
    /// The mouse event.
    /// </summary>
    public class MouseEvent : Event
    {
        /// <summary>
        /// Gets or sets the container point.
        /// </summary>
        public PointF ContainerPoint { get; set; }

        /// <summary>
        /// Gets or sets the lat lng.
        /// </summary>
        public LatLng LatLng { get; set; }

        /// <summary>
        /// Gets or sets the layer point.
        /// </summary>
        public PointF LayerPoint { get; set; }
    }
}