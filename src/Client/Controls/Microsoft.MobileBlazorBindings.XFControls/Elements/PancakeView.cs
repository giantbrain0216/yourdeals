// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PancakeView.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using XF = Xamarin.Forms;

#endregion

namespace Microsoft.MobileBlazorBindings.XFControls.Elements
{
    #region

    using Microsoft.AspNetCore.Components;
    using Microsoft.MobileBlazorBindings.Core;
    using Microsoft.MobileBlazorBindings.Elements;
    using Microsoft.MobileBlazorBindings.XFControls.Elements.Handlers;

    using Xamarin.Forms.PancakeView;

    #endregion

    /// <summary>
    /// The pancake view.
    /// </summary>
    //NOSONAR lightweight logging
    public class PancakeView : ContentView
    {
        /// <summary>
        /// Initializes static members of the <see cref="PancakeView"/> class.
        /// </summary>
        static PancakeView()
        {
            ElementHandlerRegistry.RegisterElementHandler<PancakeView>(
                renderer => new PancakeViewHandler(renderer, new Xamarin.Forms.PancakeView.PancakeView()));
        }

        /// <summary>
        /// Gets or sets the background gradient angle.
        /// </summary>
        [Parameter]
        public int? BackgroundGradientAngle { get; set; }

        /// <summary>
        /// Gets or sets the background gradient end color.
        /// </summary>
        [Parameter]
        public XF.Color? BackgroundGradientEndColor { get; set; }

        /// <summary>
        /// Gets or sets the background gradient start color.
        /// </summary>
        [Parameter]
        public XF.Color? BackgroundGradientStartColor { get; set; }

        /// <summary>
        /// Gets or sets the border color.
        /// </summary>
        [Parameter]
        public XF.Color? BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the border drawing style.
        /// </summary>
        [Parameter]
        public BorderDrawingStyle? BorderDrawingStyle { get; set; }

        /// <summary>
        /// Gets or sets the border gradient angle.
        /// </summary>
        [Parameter]
        public int? BorderGradientAngle { get; set; }

        /// <summary>
        /// Gets or sets the border gradient end color.
        /// </summary>
        [Parameter]
        public XF.Color? BorderGradientEndColor { get; set; }

        /// <summary>
        /// Gets or sets the border gradient start color.
        /// </summary>
        [Parameter]
        public XF.Color? BorderGradientStartColor { get; set; }

        /// <summary>
        /// Gets or sets the border is dashed.
        /// </summary>
        [Parameter]
        public bool? BorderIsDashed { get; set; }

        /// <summary>
        /// Gets or sets the border thickness.
        /// </summary>
        [Parameter]
        public float? BorderThickness { get; set; }

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        [Parameter]
        public XF.CornerRadius? CornerRadius { get; set; }

        /// <summary>
        /// Gets or sets the elevation.
        /// </summary>
        [Parameter]
        public int? Elevation { get; set; }

        /// <summary>
        /// Gets or sets the has shadow.
        /// </summary>
        [Parameter]
        public bool? HasShadow { get; set; }

        /// <summary>
        /// Gets or sets the offset angle.
        /// </summary>
        [Parameter]
        public double? OffsetAngle { get; set; }

        /// <summary>
        /// Gets or sets the sides.
        /// </summary>
        [Parameter]
        public int? Sides { get; set; }

        /// <summary>
        /// The render attributes.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (this.BackgroundGradientAngle != null)
                builder.AddAttribute(nameof(this.BackgroundGradientAngle), this.BackgroundGradientAngle.Value);
            if (this.BackgroundGradientEndColor != null)
                builder.AddAttribute(
                    nameof(this.BackgroundGradientEndColor),
                    AttributeHelper.ColorToString(this.BackgroundGradientEndColor.Value));
            if (this.BackgroundGradientStartColor != null)
                builder.AddAttribute(
                    nameof(this.BackgroundGradientStartColor),
                    AttributeHelper.ColorToString(this.BackgroundGradientStartColor.Value));

            // IEnumerable<GradientStop> BackgroundGradientStops
            if (this.BorderColor != null)
                builder.AddAttribute(nameof(this.BorderColor), AttributeHelper.ColorToString(this.BorderColor.Value));
            if (this.BorderDrawingStyle != null)
                builder.AddAttribute(nameof(this.BorderDrawingStyle), (int)this.BorderDrawingStyle.Value);
            if (this.BorderGradientAngle != null)
                builder.AddAttribute(nameof(this.BorderGradientAngle), this.BorderGradientAngle.Value);
            if (this.BorderGradientEndColor != null)
                builder.AddAttribute(
                    nameof(this.BorderGradientEndColor),
                    AttributeHelper.ColorToString(this.BorderGradientEndColor.Value));
            if (this.BorderGradientStartColor != null)
                builder.AddAttribute(
                    nameof(this.BorderGradientStartColor),
                    AttributeHelper.ColorToString(this.BorderGradientStartColor.Value));

            // IEnumerable<GradientStop> BorderGradientStops
            if (this.BorderIsDashed != null)
                builder.AddAttribute(nameof(this.BorderIsDashed), this.BorderIsDashed.Value);
            if (this.BorderThickness != null)
                builder.AddAttribute(
                    nameof(this.BorderThickness),
                    AttributeHelper.SingleToString(this.BorderThickness.Value));
            if (this.CornerRadius != null)
                builder.AddAttribute(
                    nameof(this.CornerRadius),
                    AttributeHelper.CornerRadiusToString(this.CornerRadius.Value));
            if (this.Elevation != null) builder.AddAttribute(nameof(this.Elevation), this.Elevation.Value);
            if (this.HasShadow != null) builder.AddAttribute(nameof(this.HasShadow), this.HasShadow.Value);
            if (this.OffsetAngle != null)
                builder.AddAttribute(nameof(this.OffsetAngle), AttributeHelper.DoubleToString(this.OffsetAngle.Value));
            if (this.Sides != null) builder.AddAttribute(nameof(this.Sides), this.Sides.Value);
        }
    }
}