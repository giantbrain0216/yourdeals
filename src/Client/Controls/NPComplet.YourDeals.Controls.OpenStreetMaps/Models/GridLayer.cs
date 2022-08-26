﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridLayer.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models
{
    #region

    using System;
    using System.Drawing;

    #endregion

    /// <summary>
    /// The grid layer.
    /// </summary>
    public abstract class GridLayer : Layer
    {
        /// <summary>
        ///     If set, tiles will only be loaded inside the set.
        /// </summary>
        public Tuple<LatLng, LatLng> Bounds { get; set; }

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        public double Opacity { get; set; } = 1.0;

        /// <summary>
        ///     Width and height of tiles in the grid.
        /// </summary>
        public Size TileSize { get; set; } = new Size(256, 256);

        /// <summary>
        ///     Tiles will not update more than once every updateInterval milliseconds when panning.
        /// </summary>
        public int UpdateInterval { get; set; } = 200;

        /// <summary>
        ///     By default, a smooth zoom animation will update grid layers every integer zoom level. Setting this option to false
        ///     will update the grid layer only when the smooth animation ends.
        /// </summary>
        public bool UpdateWhenZooming { get; set; } = true;

        /// <summary>
        /// Gets or sets the z index.
        /// </summary>
        public int ZIndex { get; set; } = 1;
    }
}