// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MobileProfileHeader.razor.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Client.Shared.Components
{
    #region

    using Microsoft.AspNetCore.Components;

    using SkiaSharp;
    using SkiaSharp.Views.Forms;

    #endregion

    /// <summary>
    /// The mobile profile header.
    /// </summary>
    public partial class MobileProfileHeader
    {
        private const float TextOffset = 1.25f;

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [Parameter]
        public float Height { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        [Parameter]
        public string Text { get; set; }

        /// <summary>
        /// The get color for text.
        /// </summary>
        /// <returns>
        /// The <see cref="SKColor"/>.
        /// </returns>
        public SKColor GetColorForText()
        {
            // determine color based on Text property
            return SKColors.Orange;
        }

        private void OnPainting(SKPaintSurfaceEventArgs e)
        {
            var surface = e.Surface;

            var canvas = surface.Canvas;

            canvas.Clear();

            var circleFill = new SKPaint
                                 {
                                     IsAntialias = true, Style = SKPaintStyle.Fill, Color = this.GetColorForText()
                                 };

            canvas.DrawCircle(this.Height, this.Height, this.Height, circleFill);

            var textPaint = new SKPaint
                                {
                                    IsAntialias = true,
                                    Style = SKPaintStyle.Fill,
                                    Color = SKColors.White,
                                    TextSize = this.Height / TextOffset,
                                    TextAlign = SKTextAlign.Center
                                };

            canvas.DrawText(this.Text, this.Height, this.Height * TextOffset, textPaint);
        }
    }
}