// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ErrorEvent.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models.Events
{
    /// <summary>
    /// The error event.
    /// </summary>
    public class ErrorEvent : Event
    {
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }
    }
}