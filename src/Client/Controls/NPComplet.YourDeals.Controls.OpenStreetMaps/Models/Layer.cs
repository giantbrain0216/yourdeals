// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Layer.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models
{
    #region

    using Microsoft.JSInterop;

    using NPComplet.YourDeals.Controls.OpenStreetMaps.Models.Events;
    using NPComplet.YourDeals.Controls.OpenStreetMaps.Utils;

    #endregion

    /// <summary>
    /// The layer.
    /// </summary>
    public abstract class Layer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Layer"/> class.
        /// </summary>
        protected Layer()
        {
            this.Id = StringHelper.GetRandomString(20);
        }

        /// <summary>
        /// The event handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public delegate void EventHandler(Layer sender, Event e);

        /// <summary>
        /// The popup event handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public delegate void PopupEventHandler(Layer sender, PopupEvent e);

        /// <summary>
        /// The tooltip event handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public delegate void TooltipEventHandler(Layer sender, TooltipEvent e);

        /// <summary>
        /// The on add.
        /// </summary>
        public event EventHandler OnAdd;

        /// <summary>
        /// The on popup close.
        /// </summary>
        public event PopupEventHandler OnPopupClose;

        /// <summary>
        /// The on popup open.
        /// </summary>
        public event PopupEventHandler OnPopupOpen;

        /// <summary>
        /// The on remove.
        /// </summary>
        public event EventHandler OnRemove;

        /// <summary>
        /// The on tooltip close.
        /// </summary>
        public event TooltipEventHandler OnTooltipClose;

        /// <summary>
        /// The on tooltip open.
        /// </summary>
        public event TooltipEventHandler OnTooltipOpen;

        /// <summary>
        ///     String to be shown in the attribution control, e.g. "© OpenStreetMap contributors". It describes the layer data and
        ///     is often a legal obligation towards copyright holders and tile providers.
        /// </summary>
        public string Attribution { get; set; }

        /// <summary>
        ///     Unique identifier used by the interoperability service on the client side to identify layers.
        /// </summary>
        public string Id { get; }

        /// <summary>
        ///     By default the layer will be added to the map's overlay pane. Overriding this option will cause the layer to be
        ///     placed on another pane by default.
        /// </summary>
        public virtual string Pane { get; set; } = "overlayPane";

        /// <summary>
        ///     The popup shown when the marker is clicked.
        /// </summary>
        public Popup Popup { get; set; }

        /// <summary>
        ///     The tooltip assigned to this marker.
        /// </summary>
        public Tooltip Tooltip { get; set; }

        /// <summary>
        /// The notify add.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyAdd(Event eventArgs)
        {
            this.OnAdd?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify popup close.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyPopupClose(PopupEvent eventArgs)
        {
            this.OnPopupClose?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify popup open.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyPopupOpen(PopupEvent eventArgs)
        {
            this.OnPopupOpen?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify remove.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyRemove(Event eventArgs)
        {
            this.OnRemove?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify tooltip close.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyTooltipClose(TooltipEvent eventArgs)
        {
            this.OnTooltipClose?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify tooltip open.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyTooltipOpen(TooltipEvent eventArgs)
        {
            this.OnTooltipOpen?.Invoke(this, eventArgs);
        }
    }
}