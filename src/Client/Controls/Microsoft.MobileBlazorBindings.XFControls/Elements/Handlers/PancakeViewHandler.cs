// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PancakeViewHandler.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using XF = Xamarin.Forms;

#endregion

namespace Microsoft.MobileBlazorBindings.XFControls.Elements.Handlers
{
    #region

    using System;

    using Microsoft.MobileBlazorBindings.Core;
    using Microsoft.MobileBlazorBindings.Elements;
    using Microsoft.MobileBlazorBindings.Elements.Handlers;

    using Xamarin.Forms.PancakeView;

    #endregion

    /// <summary>
    /// The pancake view handler.
    /// </summary>
    //NOSONAR lightweight logging
    public class PancakeViewHandler : ContentViewHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PancakeViewHandler"/> class.
        /// </summary>
        /// <param name="renderer">
        /// The renderer.
        /// </param>
        /// <param name="pancakeViewControl">
        /// The pancake view control.
        /// </param>
        public PancakeViewHandler(NativeComponentRenderer renderer, PancakeView pancakeViewControl)
            : base(renderer, pancakeViewControl)
        {
            this.PancakeViewControl = pancakeViewControl ?? throw new ArgumentNullException(nameof(pancakeViewControl));
        }

        /// <summary>
        /// Gets the pancake view control.
        /// </summary>
        public PancakeView PancakeViewControl { get; }

        /// <summary>
        /// The apply attribute.
        /// </summary>
        /// <param name="attributeEventHandlerId">
        /// The attribute event handler id.
        /// </param>
        /// <param name="attributeName">
        /// The attribute name.
        /// </param>
        /// <param name="attributeValue">
        /// The attribute value.
        /// </param>
        /// <param name="attributeEventUpdatesAttributeName">
        /// The attribute event updates attribute name.
        /// </param>
        public override void ApplyAttribute(
            ulong attributeEventHandlerId,
            string attributeName,
            object attributeValue,
            string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(PancakeView.BackgroundGradientAngle):
                    this.PancakeViewControl.BackgroundGradientAngle = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(PancakeView.BackgroundGradientEndColor):
                    this.PancakeViewControl.BackgroundGradientEndColor =
                        AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(PancakeView.BackgroundGradientStartColor):
                    this.PancakeViewControl.BackgroundGradientStartColor =
                        AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(PancakeView.BorderColor):
                    this.PancakeViewControl.BorderColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(PancakeView.BorderDrawingStyle):
                    this.PancakeViewControl.BorderDrawingStyle =
                        (BorderDrawingStyle)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(PancakeView.BorderGradientAngle):
                    this.PancakeViewControl.BorderGradientAngle = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(PancakeView.BorderGradientEndColor):
                    this.PancakeViewControl.BorderGradientEndColor =
                        AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(PancakeView.BorderGradientStartColor):
                    this.PancakeViewControl.BorderGradientStartColor =
                        AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(PancakeView.BorderIsDashed):
                    this.PancakeViewControl.BorderIsDashed = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(PancakeView.BorderThickness):
                    this.PancakeViewControl.BorderThickness = AttributeHelper.StringToSingle((string)attributeValue);
                    break;
                case nameof(PancakeView.CornerRadius):
                    this.PancakeViewControl.CornerRadius = AttributeHelper.StringToCornerRadius(attributeValue);
                    break;
                case nameof(PancakeView.Elevation):
                    this.PancakeViewControl.Elevation = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(PancakeView.HasShadow):
                    this.PancakeViewControl.HasShadow = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(PancakeView.OffsetAngle):
                    this.PancakeViewControl.OffsetAngle = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(PancakeView.Sides):
                    this.PancakeViewControl.Sides = AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(
                        attributeEventHandlerId,
                        attributeName,
                        attributeValue,
                        attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}