// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DragEvent.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models.Events
{
    /// <summary>
    /// The drag event.
    /// </summary>
    public class DragEvent : Event
    {
        /// <summary>
        /// Gets or sets the lat lng.
        /// </summary>
        public LatLng LatLng { get; set; }

        /// <summary>
        /// Gets or sets the old lat lng.
        /// </summary>
        public LatLng OldLatLng { get; set; }
    }
}