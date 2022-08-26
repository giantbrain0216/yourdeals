// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MobileNativeMapRenderer.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using Microsoft.MobileBlazorBindings.XFControls.CustomControls;

using NPComplet.YourDeals.Client.Android.Renderers;

using Xamarin.Forms;

#endregion

[assembly: ExportRenderer(typeof(MobileNativeMap), typeof(MobileNativeMapRenderer))]

namespace NPComplet.YourDeals.Client.Android.Renderers
{
    #region

    using System;
    using System.Collections.Generic;

    using global::Android.App;
    using global::Android.Content;
    using global::Android.Gms.Maps;
    using global::Android.Gms.Maps.Model;
    using global::Android.Views;
    using global::Android.Widget;

    using Microsoft.MobileBlazorBindings.XFControls.CustomControls;

    using Xamarin.Forms.Maps;
    using Xamarin.Forms.Maps.Android;
    using Xamarin.Forms.Platform.Android;

    using Resource = NPComplet.YourDeals.Client.Android.Resource;

    #endregion

    /// <summary>
    /// The mobile native map renderer.
    /// </summary>
    public class MobileNativeMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        private List<NPCompletPin> customPins;

        /// <summary>
        /// Initializes a new instance of the <see cref="MobileNativeMapRenderer"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public MobileNativeMapRenderer(Context context)
            : base(context)
        {
        }

        /// <summary>
        /// The get info contents.
        /// </summary>
        /// <param name="marker">
        /// The marker.
        /// </param>
        /// <returns>
        /// The <see cref="View"/>.
        /// </returns>
        /// <exception cref="Exception">
        /// </exception>
        public View GetInfoContents(Marker marker)
        {
            var inflater = Application.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            if (inflater != null)
            {
                var customPin = this.GetCustomPin(marker);
                if (customPin == null) throw new Exception("Custom pin not found");

                var view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);

                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
                var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

                if (infoTitle != null) infoTitle.Text = marker.Title;
                if (infoSubtitle != null) infoSubtitle.Text = marker.Snippet;

                return view;
            }

            return null;
        }

        /// <summary>
        /// The get info window.
        /// </summary>
        /// <param name="marker">
        /// The marker.
        /// </param>
        /// <returns>
        /// The <see cref="View"/>.
        /// </returns>
        public View GetInfoWindow(Marker marker)
        {
            return null;
        }

        /// <summary>
        /// The create marker.
        /// </summary>
        /// <param name="pin">
        /// The pin.
        /// </param>
        /// <returns>
        /// The <see cref="MarkerOptions"/>.
        /// </returns>
        protected override MarkerOptions CreateMarker(Pin pin)
        {
            
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            
            marker.SetSnippet(pin.Address);

            var cutompin = GetCustomPin(pin);

            if (cutompin.DealType == DealType.Offer)
            {
                var bmDescriptor = BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan);
                marker.SetIcon(bmDescriptor);
            }
            else
            {
                var bmDescriptor = BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueOrange);
                marker.SetIcon(bmDescriptor);
            }
          
            
            return marker;
        }

        /// <summary>
        /// The on element changed.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
            }

            if (e.NewElement != null)
            {
                var formsMap = (MobileNativeMap)e.NewElement;
                this.customPins = formsMap.NPCompletPins;
            }
        }

        /// <summary>
        /// The on map ready.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            this.NativeMap.SetInfoWindowAdapter(this);
        }
        private NPCompletPin GetCustomPin(Pin pinparamter)
        {
            var position = new Position(pinparamter.Position.Latitude, pinparamter.Position.Longitude);
            foreach (var pin in this.customPins)
                if (pin.Position == position&&pin.AutomationId==pinparamter.AutomationId)
                    return pin;
            return null;
        }
        private NPCompletPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (var pin in this.customPins)
                if (pin.Position == position)
                    return pin;
            return null;
        }
    }
}