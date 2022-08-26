// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Maps.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using XF = Xamarin.Forms;

#endregion

namespace Microsoft.MobileBlazorBindings.XFControls.Elements
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Components;
    using Microsoft.MobileBlazorBindings.Core;
    using Microsoft.MobileBlazorBindings.Elements;
    using Microsoft.MobileBlazorBindings.XFControls.CustomControls;
    using Microsoft.MobileBlazorBindings.XFControls.Elements.Handlers;

    using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
    using NPComplet.YourDeals.Domain.Shared.Deal.Requests;
    using NPComplet.YourDeals.Domain.Shared.Search;

    using Xamarin.Forms.Maps;

    #endregion

    /// <summary>
    /// The maps.
    /// </summary>
    //NOSONAR lightweight logging
    public class Maps : View
    {
        /// <summary>
        /// Initializes static members of the <see cref="Maps"/> class.
        /// </summary>
        static Maps()
        {
            ElementHandlerRegistry.RegisterElementHandler<Maps>(
                renderer => new MapsHandler(renderer, new MobileNativeMap()));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Maps"/> class.
        /// </summary>
        public Maps()
        {
            this.NPCompletPins = new List<NPCompletPin>();
        }

        /// <summary>
        /// Gets or sets the initial view.
        /// </summary>
        [Parameter]
        public MapSpan InitialView { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is search.
        /// </summary>
        [Parameter]
        public bool IsSearch { get; set; }

        /// <summary>
        /// Gets or sets the map type.
        /// </summary>
        [Parameter]
        public MapType? MapType { get; set; }

        /// <summary>
        /// Gets or sets the np complet pins.
        /// </summary>
        [Parameter]
        public List<NPCompletPin> NPCompletPins { get; set; }

        /// <summary>
        /// Gets or sets the offers pins.
        /// </summary>
        [Parameter]
        public List<Offer> OffersPins { get; set; }

        /// <summary>
        /// Gets or sets the pins.
        /// </summary>
        [Parameter]
        public List<NPCompletPin> Pins { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        [Parameter]
        public Position Position { get; set; }

        /// <summary>
        /// Gets or sets the requests pins.
        /// </summary>
        [Parameter]
        public List<Request> RequestsPins { get; set; }

        /// <summary>
        /// Gets or sets the selected tag.
        /// </summary>
        [Parameter]
        public Tag SelectedTag { get; set; }

        /// <summary>
        /// Gets or sets the shell.
        /// </summary>
        [Parameter]
        public XF.Shell Shell { get; set; }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        [Parameter]
        public XF.Button View { get; set; }

        /// <summary>
        /// The render attributes.
        /// </summary>
        /// <param name="builder">
        /// The builder.
        /// </param>
        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (this.MapType != null) builder.AddAttribute(nameof(this.MapType), this.MapType.Value);
            if (this.View == null)
                this.View = new XF.Button { WidthRequest = 20, HeightRequest = 20, Text = "Test" };

            if (this.View != null && this.Shell != null)
                try
                {
                    XF.Shell.SetTitleView(this.Shell, this.View);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            if (this.OffersPins != null && this.OffersPins.Count > 0)
            {
                this.OffersPins.ForEach(
                    delegate(Offer offer)
                        {
                            var pin = new NPCompletPin
                                          {
                                              Type = PinType.Place,
                                              Label = "new",
                                              Address = offer.Address.StreetAddress,
                                              Position = new Position(
                                                  offer.Address.Location.Latitude,
                                                  offer.Address.Location.Longitude),
                                              AutomationId = offer.Id.ToString(),
                                              Name = offer.Id.ToString(),
                                              DealType = DealType.Offer,
                                              Tags = offer.SearchTags.ToList()
                                          };
                            pin.MarkerClicked += (s, e) =>
                                {
                                    e.HideInfoWindow = true;
                                    XF.MessagingCenter.Instance.Send(pin, "OpenDeal");
                                };
                            this.NPCompletPins.Add(pin);
                        });
                builder.AddAttribute(nameof(this.NPCompletPins), AttributeHelper.ObjectToDelegate(this.NPCompletPins));
            }

            if (this.RequestsPins != null && this.RequestsPins.Count > 0)
            {
                this.RequestsPins.ForEach(
                    delegate(Request req)
                        {
                            var pin = new NPCompletPin
                                          {
                                              Type = PinType.Place,
                                              Label = "new",
                                              Address = req.Address.StreetAddress,
                                              Position = new Position(
                                                  req.Address.Location.Latitude,
                                                  req.Address.Location.Longitude),
                                              AutomationId = req.Id.ToString(),
                                              Name = req.Id.ToString(),
                                              DealType = DealType.Request,
                                              Tags = req.SearchTags.ToList()
                                          };
                            pin.MarkerClicked += (s, e) =>
                                {
                                    e.HideInfoWindow = true;
                                    XF.MessagingCenter.Instance.Send(pin, "OpenDeal");
                                };
                            this.NPCompletPins.Add(pin);
                        });
                builder.AddAttribute(nameof(this.NPCompletPins), AttributeHelper.ObjectToDelegate(this.NPCompletPins));
            }

            if (this.InitialView != null)
                builder.AddAttribute(nameof(this.InitialView), AttributeHelper.ObjectToDelegate(this.InitialView));

            if (this.SelectedTag != null)
                builder.AddAttribute(nameof(this.SelectedTag), AttributeHelper.ObjectToDelegate(this.SelectedTag));
        }
    }
}