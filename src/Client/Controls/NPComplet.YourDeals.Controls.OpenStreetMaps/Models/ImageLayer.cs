// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageLayer.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Controls.OpenStreetMaps.Models
{
    #region

    using System.Drawing;

    #endregion

    /// <summary>
    /// The image layer.
    /// </summary>
    public class ImageLayer : InteractiveLayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageLayer"/> class.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="corner1">
        /// The corner 1.
        /// </param>
        /// <param name="corner2">
        /// The corner 2.
        /// </param>
        public ImageLayer(string url, PointF corner1, PointF corner2)
        {
            this.Url = url;
            this.Corner1 = corner1;
            this.Corner2 = corner2;
        }

        /// <summary>
        ///     Text for the alt attribute of the image (useful for accessibility).
        /// </summary>
        public string Alt { get; set; } = string.Empty;

        /// <summary>
        ///     A custom class name to assign to the image. Empty by default.
        /// </summary>
        public string ClassName { get; set; } = string.Empty;

        /// <summary>
        /// Gets the corner 1.
        /// </summary>
        public PointF Corner1 { get; }

        /// <summary>
        /// Gets the corner 2.
        /// </summary>
        public PointF Corner2 { get; }

        /// <summary>
        ///     Whether the crossOrigin attribute will be added to the image. If a String is provided, the image will have its
        ///     crossOrigin attribute set to the String provided. This is needed if you want to access image pixel data. Refer to
        ///     CORS Settings for valid String values.
        /// </summary>
        public string CrossOrigin { get; set; }

        /// <summary>
        ///     URL to the overlay image to show in place of the overlay that failed to load.
        /// </summary>
        public string ErrorOverlayUrl { get; set; } = string.Empty;

        /// <summary>
        ///     The opacity of the image overlay.
        /// </summary>
        public float Opacity { get; set; } = 1.0f;

        /// <summary>
        ///     Image url.
        /// </summary>
        public string Url { get; }

        /// <summary>
        ///     The explicit zIndex of the overlay layer.
        /// </summary>
        public int zIndex { get; set; } = 1;
    }
}