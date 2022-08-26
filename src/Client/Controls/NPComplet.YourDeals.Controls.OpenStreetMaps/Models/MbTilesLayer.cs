﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MbTilesLayer.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models
{
    /// <summary>
    ///     Class for .mbtiles tilesets. Requires the Leaflet.TileLayer.MBTiles plugin
    /// </summary>
    public class MbTilesLayer : Layer
    {
        /// <summary>
        ///     The maximum zoom level up to which this layer will be displayed (inclusive).
        /// </summary>
        public float MaximumZoom { get; set; } = 18;

        /// <summary>
        ///     The minimum zoom level down to which this layer will be displayed (inclusive).
        /// </summary>
        public float MinimumZoom { get; set; }

        /// <summary>
        ///     Instantiates a tile layer object given a URL template.
        /// </summary>
        public string UrlTemplate { get; set; }
    }
}