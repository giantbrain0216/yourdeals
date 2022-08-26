// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapsHandler.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.MobileBlazorBindings.XFControls.Elements.Handlers
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.MobileBlazorBindings.Core;
    using Microsoft.MobileBlazorBindings.Elements;
    using Microsoft.MobileBlazorBindings.Elements.Handlers;
    using Microsoft.MobileBlazorBindings.XFControls.CustomControls;

    using NPComplet.YourDeals.Domain.Shared.Search;

    using Xamarin.Forms.Maps;

    #endregion

    /// <summary>
    /// The maps handler.
    /// </summary>
    public class MapsHandler : ViewHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapsHandler"/> class.
        /// </summary>
        /// <param name="renderer">
        /// The renderer.
        /// </param>
        /// <param name="mapsControl">
        /// The maps control.
        /// </param>
        public MapsHandler(NativeComponentRenderer renderer, MobileNativeMap mapsControl)
            : base(renderer, mapsControl)
        {
            this.MapsControl = mapsControl ?? throw new ArgumentNullException(nameof(mapsControl));
        }

        /// <summary>
        /// Gets the maps control.
        /// </summary>
        public MobileNativeMap MapsControl { get; }

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
                case nameof(MobileNativeMap.InitialView):
                    this.MapsControl.MoveToRegion(AttributeHelper.DelegateToObject<MapSpan>(attributeValue));
                    break;

                case "SelectedTag":
                    this.MapsControl.Pins.Clear();

                    var tag = AttributeHelper.DelegateToObject<Tag>(attributeValue);
                    this.MapsControl.NPCompletPins.ForEach(
                        delegate(NPCompletPin pin)
                            {
                                if (string.IsNullOrWhiteSpace(tag.TagString)
                                    || pin.Tags.Any(p => p.TagString.Contains(tag.TagString)))
                                    this.MapsControl.Pins.Add(pin);
                            });
                    break;

                case nameof(MobileNativeMap.MapType):
                    this.MapsControl.MapType = MapType.Street;
                    break;

                case nameof(MobileNativeMap.NPCompletPins):
                    var mapCustomPins = AttributeHelper.DelegateToObject<List<NPCompletPin>>(attributeValue);

                    this.MapsControl.NPCompletPins.Clear();
                    this.MapsControl.Pins.Clear();

                    mapCustomPins.ForEach(
                        pin =>
                            {
                                this.MapsControl.NPCompletPins.Add(pin);
                                this.MapsControl.Pins.Add(pin);
                            });
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