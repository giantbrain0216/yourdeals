// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Marker.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models
{
    #region

    using System.Drawing;

    using Microsoft.JSInterop;

    using NPComplet.YourDeals.Controls.OpenStreetMaps.Models.Events;

    #endregion

    /// <summary>
    /// The marker.
    /// </summary>
    public class Marker : InteractiveLayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Marker"/> class.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        public Marker(float x, float y)
            : this(new LatLng(x, y))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Marker"/> class.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        public Marker(PointF position)
            : this(position.X, position.Y)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Marker"/> class.
        /// </summary>
        /// <param name="latLng">
        /// The lat lng.
        /// </param>
        public Marker(LatLng latLng)
        {
            this.Position = latLng;
        }

        /// <summary>
        /// The drag end event handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public delegate void DragEndEventHandler(Marker sender, DragEndEvent e);

        /// <summary>
        /// The drag event handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public delegate void DragEventHandler(Marker sender, DragEvent e);

        /// <summary>
        /// The event handler marker.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public delegate void EventHandlerMarker(Marker sender, Event e);

        /// <summary>
        /// The on drag.
        /// </summary>
        public event DragEventHandler OnDrag;

        /// <summary>
        /// The on drag end.
        /// </summary>
        public event DragEndEventHandler OnDragEnd;

        /// <summary>
        /// The on drag start.
        /// </summary>
        public event EventHandlerMarker OnDragStart;

        /// <summary>
        /// The on move.
        /// </summary>
        public event DragEventHandler OnMove;

        /// <summary>
        /// The on move end.
        /// </summary>
        public event EventHandlerMarker OnMoveEnd;

        /// <summary>
        /// The on move start.
        /// </summary>
        public event EventHandlerMarker OnMoveStart;

        /// <summary>
        ///     Text for the alt attribute of the icon image (useful for accessibility).
        /// </summary>
        public string Alt { get; set; } = string.Empty;

        /// <summary>
        ///     Distance (in pixels to the left/right and to the top/bottom) of the map edge to start panning the map.
        /// </summary>
        public Point AutoPanPadding { get; set; } = new Point(50, 50);

        /// <summary>
        ///     Number of pixels the map should pan by.
        /// </summary>
        public int AutoPanSpeed { get; set; } = 10;

        /// <summary>
        ///     Whether the marker is draggable with mouse/touch or not.
        /// </summary>
        public bool Draggable { get; set; }

        /// <summary>
        ///     Icon instance to use for rendering the marker. See
        ///     <see href="https://leafletjs.com/reference-1.5.0.html#icon">Icon documentation</see> for details on how to
        ///     customize the marker icon. If not specified, a common instance of
        ///     <see href="https://leafletjs.com/reference-1.5.0.html#icon-default">L.Icon.Default</see> is used.
        /// </summary>
        public Icon Icon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is bubbling mouse events.
        /// </summary>
        public override bool IsBubblingMouseEvents { get; set; } = false;

        /// <summary>
        ///     Whether the marker can be tabbed to with a keyboard and clicked by pressing enter.
        /// </summary>
        public bool IsKeyboardAccessible { get; set; } = true;

        /// <summary>
        ///     The opacity of the marker.
        /// </summary>
        public double Opacity { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the pane.
        /// </summary>
        public override string Pane { get; set; } = "markerPane";

        /// <summary>
        ///     The position of the marker on the map.
        /// </summary>
        public LatLng Position { get; set; }

        /// <summary>
        ///     The z-index offset used for the riseOnHover feature.
        /// </summary>
        public int RiseOffset { get; set; } = 250;

        /// <summary>
        ///     If true, the marker will get on top of others when you hover the mouse over it.
        /// </summary>
        public bool RiseOnHover { get; set; }

        /// <summary>
        ///     Text for the browser tooltip that appear on marker hover (no tooltip by default).
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        ///     Whether to pan the map when dragging this marker near its edge or not.
        /// </summary>
        public bool UseAutoPan { get; set; }

        /// <summary>
        ///     By default, marker images zIndex is set automatically based on its latitude. Use this option if you want to put the
        ///     marker on top of all others (or below), specifying a high value like 1000 (or high negative value, respectively).
        /// </summary>
        public int ZIndexOffset { get; set; }

        /// <summary>
        /// The notify drag.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyDrag(DragEvent eventArgs)
        {
            this.OnDrag?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify drag end.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyDragEnd(DragEndEvent eventArgs)
        {
            this.OnDragEnd?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify drag start.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyDragStart(Event eventArgs)
        {
            this.OnDragStart?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify move.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyMove(DragEvent eventArgs)
        {
            this.OnMove?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify move end.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyMoveEnd(Event eventArgs)
        {
            this.OnMoveEnd?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify move start.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyMoveStart(Event eventArgs)
        {
            this.OnMoveStart?.Invoke(this, eventArgs);
        }
    }
}