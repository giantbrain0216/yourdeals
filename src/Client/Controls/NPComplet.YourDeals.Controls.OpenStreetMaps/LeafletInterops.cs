// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LeafletInterops.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps
{
    #region

    using System;
    using System.Collections.Concurrent;
    using System.Drawing;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.JSInterop;

    using NPComplet.YourDeals.Controls.OpenStreetMaps.Models;

    using Rectangle = NPComplet.YourDeals.Controls.OpenStreetMaps.Models.Rectangle;

    #endregion

    /// <summary>
    /// The leaflet interops.
    /// </summary>
    public static class LeafletInterops
    {
        private static readonly string _BaseObjectContainer = "window.leafletBlazor";

        private static ConcurrentDictionary<string, (IDisposable, string, Layer)> LayerReferences { get; } =
            new ConcurrentDictionary<string, (IDisposable, string, Layer)>();

        /// <summary>
        /// The add layer.
        /// </summary>
        /// <param name="jsRuntime">
        /// The js runtime.
        /// </param>
        /// <param name="mapId">
        /// The map id.
        /// </param>
        /// <param name="layer">
        /// The layer.
        /// </param>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public static ValueTask AddLayer(IJSRuntime jsRuntime, string mapId, Layer layer)
        {
            return layer switch
                {
                    TileLayer tileLayer => jsRuntime.InvokeVoidAsync(
                        $"{_BaseObjectContainer}.addTilelayer",
                        mapId,
                        tileLayer,
                        CreateLayerReference(mapId, tileLayer)),
                    MbTilesLayer mbTilesLayer => jsRuntime.InvokeVoidAsync(
                        $"{_BaseObjectContainer}.addMbTilesLayer",
                        mapId,
                        mbTilesLayer,
                        CreateLayerReference(mapId, mbTilesLayer)),
                    ShapefileLayer shapefileLayer => jsRuntime.InvokeVoidAsync(
                        $"{_BaseObjectContainer}.addShapefileLayer",
                        mapId,
                        shapefileLayer,
                        CreateLayerReference(mapId, shapefileLayer)),
                    Marker marker => jsRuntime.InvokeVoidAsync(
                        $"{_BaseObjectContainer}.addMarker",
                        mapId,
                        marker,
                        CreateLayerReference(mapId, marker)),
                    Rectangle rectangle => jsRuntime.InvokeVoidAsync(
                        $"{_BaseObjectContainer}.addRectangle",
                        mapId,
                        rectangle,
                        CreateLayerReference(mapId, rectangle)),
                    Circle circle => jsRuntime.InvokeVoidAsync(
                        $"{_BaseObjectContainer}.addCircle",
                        mapId,
                        circle,
                        CreateLayerReference(mapId, circle)),
                    Polygon polygon => jsRuntime.InvokeVoidAsync(
                        $"{_BaseObjectContainer}.addPolygon",
                        mapId,
                        polygon,
                        CreateLayerReference(mapId, polygon)),
                    Polyline polyline => jsRuntime.InvokeVoidAsync(
                        $"{_BaseObjectContainer}.addPolyline",
                        mapId,
                        polyline,
                        CreateLayerReference(mapId, polyline)),
                    ImageLayer image => jsRuntime.InvokeVoidAsync(
                        $"{_BaseObjectContainer}.addImageLayer",
                        mapId,
                        image,
                        CreateLayerReference(mapId, image)),
                    GeoJsonDataLayer geo => jsRuntime.InvokeVoidAsync(
                        $"{_BaseObjectContainer}.addGeoJsonLayer",
                        mapId,
                        geo,
                        CreateLayerReference(mapId, geo)),
                    _ => throw new NotImplementedException($"The layer {typeof(Layer).Name} has not been implemented.")
                };
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="jsRuntime">
        /// The js runtime.
        /// </param>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public static ValueTask Create(IJSRuntime jsRuntime, Map map)
        {
            return jsRuntime.InvokeVoidAsync($"{_BaseObjectContainer}.create", map, DotNetObjectReference.Create(map));
        }

        /// <summary>
        /// The fit bounds.
        /// </summary>
        /// <param name="jsRuntime">
        /// The js runtime.
        /// </param>
        /// <param name="mapId">
        /// The map id.
        /// </param>
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
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public static ValueTask FitBounds(
            IJSRuntime jsRuntime,
            string mapId,
            PointF corner1,
            PointF corner2,
            PointF? padding,
            float? maxZoom)
        {
            return jsRuntime.InvokeVoidAsync(
                $"{_BaseObjectContainer}.fitBounds",
                mapId,
                corner1,
                corner2,
                padding,
                maxZoom);
        }

        /// <summary>
        /// The get center.
        /// </summary>
        /// <param name="jsRuntime">
        /// The js runtime.
        /// </param>
        /// <param name="mapId">
        /// The map id.
        /// </param>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public static ValueTask<LatLng> GetCenter(IJSRuntime jsRuntime, string mapId)
        {
            return jsRuntime.InvokeAsync<LatLng>($"{_BaseObjectContainer}.getCenter", mapId);
        }

        /// <summary>
        /// The get zoom.
        /// </summary>
        /// <param name="jsRuntime">
        /// The js runtime.
        /// </param>
        /// <param name="mapId">
        /// The map id.
        /// </param>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public static ValueTask<float> GetZoom(IJSRuntime jsRuntime, string mapId)
        {
            return jsRuntime.InvokeAsync<float>($"{_BaseObjectContainer}.getZoom", mapId);
        }

        /// <summary>
        /// The pan to.
        /// </summary>
        /// <param name="jsRuntime">
        /// The js runtime.
        /// </param>
        /// <param name="mapId">
        /// The map id.
        /// </param>
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
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public static ValueTask PanTo(
            IJSRuntime jsRuntime,
            string mapId,
            PointF position,
            bool animate,
            float duration,
            float easeLinearity,
            bool noMoveStart)
        {
            return jsRuntime.InvokeVoidAsync(
                $"{_BaseObjectContainer}.panTo",
                mapId,
                position,
                animate,
                duration,
                easeLinearity,
                noMoveStart);
        }

        /// <summary>
        /// The remove layer.
        /// </summary>
        /// <param name="jsRuntime">
        /// The js runtime.
        /// </param>
        /// <param name="mapId">
        /// The map id.
        /// </param>
        /// <param name="layerId">
        /// The layer id.
        /// </param>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public static async ValueTask RemoveLayer(IJSRuntime jsRuntime, string mapId, string layerId)
        {
            await jsRuntime.InvokeVoidAsync($"{_BaseObjectContainer}.removeLayer", mapId, layerId);
            DisposeLayerReference(layerId);
        }

        /// <summary>
        /// The set zoom.
        /// </summary>
        /// <param name="jsRuntime">
        /// The js runtime.
        /// </param>
        /// <param name="mapId">
        /// The map id.
        /// </param>
        /// <param name="zoom">
        /// The zoom.
        /// </param>
        /// <param name="latLng">
        /// The lat lng.
        /// </param>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public static ValueTask SetZoom(IJSRuntime jsRuntime, string mapId, float zoom, LatLng latLng)
        {
            return jsRuntime.InvokeVoidAsync($"{_BaseObjectContainer}.setZoom", mapId, zoom, latLng);
        }

        /// <summary>
        /// The update popup content.
        /// </summary>
        /// <param name="jsRuntime">
        /// The js runtime.
        /// </param>
        /// <param name="mapId">
        /// The map id.
        /// </param>
        /// <param name="layer">
        /// The layer.
        /// </param>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public static ValueTask UpdatePopupContent(IJSRuntime jsRuntime, string mapId, Layer layer)
        {
            return jsRuntime.InvokeVoidAsync(
                $"{_BaseObjectContainer}.updatePopupContent",
                mapId,
                layer.Id,
                layer.Popup?.Content);
        }

        /// <summary>
        /// The update shape.
        /// </summary>
        /// <param name="jsRuntime">
        /// The js runtime.
        /// </param>
        /// <param name="mapId">
        /// The map id.
        /// </param>
        /// <param name="layer">
        /// The layer.
        /// </param>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public static ValueTask UpdateShape(IJSRuntime jsRuntime, string mapId, Layer layer)
        {
            return layer switch
                {
                    Rectangle rectangle => jsRuntime.InvokeVoidAsync(
                        $"{_BaseObjectContainer}.updateRectangle",
                        mapId,
                        rectangle),
                    Circle circle => jsRuntime.InvokeVoidAsync($"{_BaseObjectContainer}.updateCircle", mapId, circle),
                    Polygon polygon => jsRuntime.InvokeVoidAsync(
                        $"{_BaseObjectContainer}.updatePolygon",
                        mapId,
                        polygon),
                    Polyline polyline => jsRuntime.InvokeVoidAsync(
                        $"{_BaseObjectContainer}.updatePolyline",
                        mapId,
                        polyline),
                    _ => throw new NotImplementedException($"The layer {typeof(Layer).Name} has not been implemented.")
                };
        }

        /// <summary>
        /// The update tooltip content.
        /// </summary>
        /// <param name="jsRuntime">
        /// The js runtime.
        /// </param>
        /// <param name="mapId">
        /// The map id.
        /// </param>
        /// <param name="layer">
        /// The layer.
        /// </param>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public static ValueTask UpdateTooltipContent(IJSRuntime jsRuntime, string mapId, Layer layer)
        {
            return jsRuntime.InvokeVoidAsync(
                $"{_BaseObjectContainer}.updateTooltipContent",
                mapId,
                layer.Id,
                layer.Tooltip?.Content);
        }

        /// <summary>
        /// The zoom in.
        /// </summary>
        /// <param name="jsRuntime">
        /// The js runtime.
        /// </param>
        /// <param name="mapId">
        /// The map id.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public static ValueTask ZoomIn(IJSRuntime jsRuntime, string mapId, MouseEventArgs e)
        {
            return jsRuntime.InvokeVoidAsync($"{_BaseObjectContainer}.zoomIn", mapId, e);
        }

        /// <summary>
        /// The zoom out.
        /// </summary>
        /// <param name="jsRuntime">
        /// The js runtime.
        /// </param>
        /// <param name="mapId">
        /// The map id.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
        public static ValueTask ZoomOut(IJSRuntime jsRuntime, string mapId, MouseEventArgs e)
        {
            return jsRuntime.InvokeVoidAsync($"{_BaseObjectContainer}.zoomOut", mapId, e);
        }

        private static DotNetObjectReference<T> CreateLayerReference<T>(string mapId, T layer)
            where T : Layer
        {
            var result = DotNetObjectReference.Create(layer);
            LayerReferences.TryAdd(layer.Id, (result, mapId, layer));
            return result;
        }

        private static void DisposeLayerReference(string layerId)
        {
            if (LayerReferences.TryRemove(layerId, out var value))
                value.Item1.Dispose();
        }
    }
}