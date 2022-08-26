// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Map.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.JSInterop;

    using NPComplet.YourDeals.Controls.OpenStreetMaps.Exceptions;
    using NPComplet.YourDeals.Controls.OpenStreetMaps.Models;
    using NPComplet.YourDeals.Controls.OpenStreetMaps.Models.Events;
    using NPComplet.YourDeals.Controls.OpenStreetMaps.Utils;

    #endregion

    /// <summary>
    /// The map.
    /// </summary>
    //NOSONAR lightweight logging
    public class Map
    {
        private readonly IJSRuntime _jsRuntime;

        private readonly ObservableCollection<Layer> _layers;

        private bool _isInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="jsRuntime">
        /// The js runtime.
        /// </param>
        public Map(IJSRuntime jsRuntime)
        {
            this._jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
            this.Id = StringHelper.GetRandomString(10);
            this._layers = new ObservableCollection<Layer>();
            this._layers.CollectionChanged += this.OnLayersChanged;
        }

        /// <summary>
        /// The map event handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public delegate void MapEventHandler(object sender, Event e);

        /// <summary>
        /// The map resize event handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public delegate void MapResizeEventHandler(object sender, ResizeEvent e);

        // Has the same events as InteractiveLayer, but it is not a layer. 
        // Could place this code in its own class and make Layer inherit from that, but not every layer is interactive...
        // Is there a way to not duplicate this code?

        /// <summary>
        /// The mouse event handler.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public delegate void MouseEventHandler(Map sender, MouseEvent e);

        /// <summary>
        /// The on click.
        /// </summary>
        public event MouseEventHandler OnClick;

        /// <summary>
        /// The on context menu.
        /// </summary>
        public event MouseEventHandler OnContextMenu;

        /// <summary>
        /// The on dbl click.
        /// </summary>
        public event MouseEventHandler OnDblClick;

        /// <summary>
        ///     Event raised when the component has finished its first render.
        /// </summary>
        public event Action OnInitialized;

        /// <summary>
        /// The on key down.
        /// </summary>
        public event MapEventHandler OnKeyDown;

        /// <summary>
        /// The on key press.
        /// </summary>
        public event MapEventHandler OnKeyPress;

        /// <summary>
        /// The on key up.
        /// </summary>
        public event MapEventHandler OnKeyUp;

        /// <summary>
        /// The on load.
        /// </summary>
        public event MapEventHandler OnLoad;

        /// <summary>
        /// The on mouse down.
        /// </summary>
        public event MouseEventHandler OnMouseDown;

        /// <summary>
        /// The on mouse move.
        /// </summary>
        public event MouseEventHandler OnMouseMove;

        /// <summary>
        /// The on mouse out.
        /// </summary>
        public event MouseEventHandler OnMouseOut;

        /// <summary>
        /// The on mouse over.
        /// </summary>
        public event MouseEventHandler OnMouseOver;

        /// <summary>
        /// The on mouse up.
        /// </summary>
        public event MouseEventHandler OnMouseUp;

        /// <summary>
        /// The on move.
        /// </summary>
        public event MapEventHandler OnMove;

        /// <summary>
        /// The on move end.
        /// </summary>
        public event MapEventHandler OnMoveEnd;

        /// <summary>
        /// The on move start.
        /// </summary>
        public event MapEventHandler OnMoveStart;

        /// <summary>
        /// The on pre click.
        /// </summary>
        public event MouseEventHandler OnPreClick;

        /// <summary>
        /// The on resize.
        /// </summary>
        public event MapResizeEventHandler OnResize;

        /// <summary>
        /// The on unload.
        /// </summary>
        public event MapEventHandler OnUnload;

        /// <summary>
        /// The on view reset.
        /// </summary>
        public event MapEventHandler OnViewReset;

        /// <summary>
        /// The on zoom.
        /// </summary>
        public event MapEventHandler OnZoom;

        /// <summary>
        /// The on zoom end.
        /// </summary>
        public event MapEventHandler OnZoomEnd;

        /// <summary>
        /// The on zoom levels change.
        /// </summary>
        public event MapEventHandler OnZoomLevelsChange;

        /// <summary>
        /// The on zoom start.
        /// </summary>
        public event MapEventHandler OnZoomStart;

        /// <summary>
        ///     Initial geographic center of the map
        /// </summary>
        public LatLng Center { get; set; } = new LatLng();

        /// <summary>
        /// Gets the id.
        /// </summary>
        public string Id { get; }

        /// <summary>
        ///     When this option is set, the map restricts the view to the given
        ///     geographical bounds, bouncing the user back if the user tries to pan
        ///     outside the view.
        /// </summary>
        public Tuple<LatLng, LatLng> MaxBounds { get; set; }

        /// <summary>
        ///     Maximum zoom level of the map. If not specified and at least one
        ///     GridLayer or TileLayer is in the map, the highest of their maxZoom
        ///     options will be used instead.
        /// </summary>
        public float? MaxZoom { get; set; }

        /// <summary>
        ///     Minimum zoom level of the map. If not specified and at least one
        ///     GridLayer or TileLayer is in the map, the lowest of their minZoom
        ///     options will be used instead.
        /// </summary>
        public float? MinZoom { get; set; }

        /// <summary>
        ///     Initial map zoom level
        /// </summary>
        public float Zoom { get; set; }

        /// <summary>
        ///     Whether a zoom control is added to the map by default.
        ///     <para />
        ///     Defaults to true.
        /// </summary>
        public bool ZoomControl { get; set; } = true;

        /// <summary>
        /// Add a layer to the map.
        /// </summary>
        /// <param name="layer">
        /// The layer to be added.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when the layer is null.
        /// </exception>
        /// <exception cref="UninitializedMapException">
        /// Throws when the map has not been yet initialized.
        /// </exception>
        public void AddLayer(Layer layer)
        {
            if (layer is null) throw new ArgumentNullException(nameof(layer));

            if (!this._isInitialized) throw new UninitializedMapException();

            this._layers.Add(layer);
        }

        /// <summary>
        /// The fit bounds.
        /// </summary>
        /// <param name="corner1">
        /// The corner 1.
        /// </param>
        /// <param name="corner2">
        /// The corner 2.
        /// </param>
        /// <param name="padding">
        /// The padding.
        /// </param>
        /// <param name="maxZoom">
        /// The max zoom.
        /// </param>
        public void FitBounds(PointF corner1, PointF corner2, PointF? padding = null, float? maxZoom = null)
        {
            LeafletInterops.FitBounds(this._jsRuntime, this.Id, corner1, corner2, padding, maxZoom);
        }

        /// <summary>
        /// The get center.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<LatLng> GetCenter()
        {
            return await LeafletInterops.GetCenter(this._jsRuntime, this.Id);
        }

        /// <summary>
        ///     Get a read only collection of the current layers.
        /// </summary>
        /// <returns>A read only collection of layers.</returns>
        public IReadOnlyCollection<Layer> GetLayers()
        {
            return this._layers.ToList().AsReadOnly();
        }

        /// <summary>
        /// The get zoom.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<float> GetZoom()
        {
            return await LeafletInterops.GetZoom(this._jsRuntime, this.Id);
        }

        /// <summary>
        /// The notify click.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyClick(MouseEvent eventArgs)
        {
            this.OnClick?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify context menu.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyContextMenu(MouseEvent eventArgs)
        {
            this.OnContextMenu?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify dbl click.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyDblClick(MouseEvent eventArgs)
        {
            this.OnDblClick?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify key down.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyKeyDown(Event eventArgs)
        {
            this.OnKeyDown?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify key press.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyKeyPress(Event eventArgs)
        {
            this.OnKeyPress?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify key up.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyKeyUp(Event eventArgs)
        {
            this.OnKeyUp?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify load.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        [JSInvokable]
        public void NotifyLoad(Event e)
        {
            this.OnLoad?.Invoke(this, e);
        }

        /// <summary>
        /// The notify mouse down.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyMouseDown(MouseEvent eventArgs)
        {
            this.OnMouseDown?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify mouse move.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyMouseMove(MouseEvent eventArgs)
        {
            this.OnMouseMove?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify mouse out.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyMouseOut(MouseEvent eventArgs)
        {
            this.OnMouseOut?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify mouse over.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyMouseOver(MouseEvent eventArgs)
        {
            this.OnMouseOver?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify mouse up.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyMouseUp(MouseEvent eventArgs)
        {
            this.OnMouseUp?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify move.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        [JSInvokable]
        public void NotifyMove(Event e)
        {
            this.OnMove?.Invoke(this, e);
        }

        /// <summary>
        /// The notify move end.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        [JSInvokable]
        public void NotifyMoveEnd(Event e)
        {
            this.OnMoveEnd?.Invoke(this, e);
        }

        /// <summary>
        /// The notify move start.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        [JSInvokable]
        public void NotifyMoveStart(Event e)
        {
            this.OnMoveStart?.Invoke(this, e);
        }

        /// <summary>
        /// The notify pre click.
        /// </summary>
        /// <param name="eventArgs">
        /// The event args.
        /// </param>
        [JSInvokable]
        public void NotifyPreClick(MouseEvent eventArgs)
        {
            this.OnPreClick?.Invoke(this, eventArgs);
        }

        /// <summary>
        /// The notify resize.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        [JSInvokable]
        public void NotifyResize(ResizeEvent e)
        {
            this.OnResize?.Invoke(this, e);
        }

        /// <summary>
        /// The notify unload.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        [JSInvokable]
        public void NotifyUnload(Event e)
        {
            this.OnUnload?.Invoke(this, e);
        }

        /// <summary>
        /// The notify view reset.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        [JSInvokable]
        public void NotifyViewReset(Event e)
        {
            this.OnViewReset?.Invoke(this, e);
        }

        /// <summary>
        /// The notify zoom.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        [JSInvokable]
        public void NotifyZoom(Event e)
        {
            this.OnZoom?.Invoke(this, e);
        }

        /// <summary>
        /// The notify zoom end.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        [JSInvokable]
        public void NotifyZoomEnd(Event e)
        {
            this.OnZoomEnd?.Invoke(this, e);
        }

        /// <summary>
        /// The notify zoom levels change.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        [JSInvokable]
        public void NotifyZoomLevelsChange(Event e)
        {
            this.OnZoomLevelsChange?.Invoke(this, e);
        }

        /// <summary>
        /// The notify zoom start.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        [JSInvokable]
        public void NotifyZoomStart(Event e)
        {
            this.OnZoomStart?.Invoke(this, e);
        }

        /// <summary>
        /// The pan to.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        /// <param name="animate">
        /// The animate.
        /// </param>
        /// <param name="duration">
        /// The duration.
        /// </param>
        /// <param name="easeLinearity">
        /// The ease linearity.
        /// </param>
        /// <param name="noMoveStart">
        /// The no move start.
        /// </param>
        public void PanTo(
            PointF position,
            bool animate = false,
            float duration = 0.25f,
            float easeLinearity = 0.25f,
            bool noMoveStart = false)
        {
            LeafletInterops.PanTo(this._jsRuntime, this.Id, position, animate, duration, easeLinearity, noMoveStart);
        }

        /// <summary>
        ///     This method MUST be called only once by the Blazor component upon rendering, and never by the user.
        /// </summary>
        public void RaiseOnInitialized()
        {
            this._isInitialized = true;
            this.OnInitialized?.Invoke();
        }

        /// <summary>
        /// Remove a layer from the map.
        /// </summary>
        /// <param name="layer">
        /// The layer to be removed.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Throws when the layer is null.
        /// </exception>
        /// <exception cref="UninitializedMapException">
        /// Throws when the map has not been yet initialized.
        /// </exception>
        public void RemoveLayer(Layer layer)
        {
            if (layer is null) throw new ArgumentNullException(nameof(layer));

            if (!this._isInitialized) throw new UninitializedMapException();

            this._layers.Remove(layer);
        }

        /// <summary>
        /// The set zoom.
        /// </summary>
        /// <param name="zoom">
        /// The zoom.
        /// </param>
        /// <param name="latLng">
        /// The lat lng.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task SetZoom(float zoom, LatLng latLng)
        {
            await LeafletInterops.SetZoom(this._jsRuntime, this.Id, zoom, latLng);
        }

        /// <summary>
        /// Increases the zoom level by one notch.
        ///     If <c>shift</c> is held down, increases it by three.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ZoomIn(MouseEventArgs e)
        {
            await LeafletInterops.ZoomIn(this._jsRuntime, this.Id, e);
        }

        /// <summary>
        /// Decreases the zoom level by one notch.
        ///     If <c>shift</c> is held down, decreases it by three.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ZoomOut(MouseEventArgs e)
        {
            await LeafletInterops.ZoomOut(this._jsRuntime, this.Id, e);
        }

        private async void OnLayersChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in args.NewItems)
                {
                    var layer = item as Layer;
                    await LeafletInterops.AddLayer(this._jsRuntime, this.Id, layer);
                }
            }
            else if (args.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in args.OldItems)
                    if (item is Layer layer)
                        await LeafletInterops.RemoveLayer(this._jsRuntime, this.Id, layer.Id);
            }
            else if (args.Action == NotifyCollectionChangedAction.Replace
                     || args.Action == NotifyCollectionChangedAction.Move)
            {
                foreach (var oldItem in args.OldItems)
                    if (oldItem is Layer layer)
                        await LeafletInterops.RemoveLayer(this._jsRuntime, this.Id, layer.Id);

                foreach (var newItem in args.NewItems)
                    await LeafletInterops.AddLayer(this._jsRuntime, this.Id, newItem as Layer);
            }
        }
    }
}