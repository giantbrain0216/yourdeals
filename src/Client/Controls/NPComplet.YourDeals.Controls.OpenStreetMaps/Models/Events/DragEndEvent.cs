// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DragEndEvent.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models.Events
{
    /// <summary>
    /// The drag end event.
    /// </summary>
    public class DragEndEvent : Event
    {
        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        public float Distance { get; set; }
    }
}