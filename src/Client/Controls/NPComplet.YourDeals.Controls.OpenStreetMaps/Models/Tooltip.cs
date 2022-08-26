﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Tooltip.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models
{
    #region

    using System.Drawing;

    #endregion

    /// <summary>
    /// The tooltip.
    /// </summary>
    public class Tooltip : DivOverlay
    {
        /// <summary>
        ///     The content of the tooltip.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     Direction where to open the tooltip. Possible values are: right, left, top, bottom, center, auto. auto will
        ///     dynamically switch between right and left according to the tooltip position on the map.
        /// </summary>
        public string Direction { get; set; } = "auto";

        /// <summary>
        ///     Whether to open the tooltip permanently or only on mouseover.
        /// </summary>
        public bool IsPermanent { get; set; }

        /// <summary>
        ///     If true, the tooltip will follow the mouse instead of being fixed at the feature center.
        /// </summary>
        public bool IsSticky { get; set; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public override Point Offset { get; set; } = Point.Empty;

        /// <summary>
        ///     Tooltip container opacity.
        /// </summary>
        public double Opacity { get; set; } = 0.9;

        /// <summary>
        /// The pane.
        /// </summary>
        public override string Pane => "tooltipPane";
    }
}