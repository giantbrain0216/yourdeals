// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResizeEvent.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models.Events
{
    #region

    using System.Drawing;

    #endregion

    /// <summary>
    /// The resize event.
    /// </summary>
    public class ResizeEvent : Event
    {
        /// <summary>
        /// Gets or sets the new size.
        /// </summary>
        public PointF NewSize { get; set; }

        /// <summary>
        /// Gets or sets the old size.
        /// </summary>
        public PointF OldSize { get; set; }
    }
}