// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TooltipEvent.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models.Events
{
    /// <summary>
    /// The tooltip event.
    /// </summary>
    public class TooltipEvent : Event
    {
        /// <summary>
        /// Gets or sets the tooltip.
        /// </summary>
        public Tooltip Tooltip { get; set; }
    }
}