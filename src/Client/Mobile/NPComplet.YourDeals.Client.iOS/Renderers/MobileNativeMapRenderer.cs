// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MobileNativeMapRenderer.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

#region

using Microsoft.MobileBlazorBindings.XFControls.CustomControls;
using NPComplet.YourDeals.iOS.Renderers;
using System.Linq;
using Xamarin.Forms;

#endregion

[assembly: ExportRenderer(typeof(MobileNativeMap), typeof(MobileNativeMapRenderer))]

namespace NPComplet.YourDeals.iOS.Renderers
{
    #region

    using CoreGraphics;
    using MapKit;
    using Microsoft.MobileBlazorBindings.XFControls.CustomControls;
    using System;
    using System.Collections.Generic;
    using UIKit;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;
    using Xamarin.Forms.Maps.iOS;
    using Xamarin.Forms.Platform.iOS;

    #endregion

    /// <summary>
    /// The mobile native map renderer.
    /// </summary>
    public class MobileNativeMapRenderer : MapRenderer
    {
        private List<NPCompletPin> _customPins;

        private UIView _customPinView;

        /// <summary>
        /// The get view for annotation.
        /// </summary>
        /// <param name="mapView"> The map view. </param>
        /// <param name="annotation"> The annotation. </param>
        /// <returns> The <see cref="MKAnnotationView"/>. </returns>
        /// <exception cref="Exception"> </exception>
        protected override MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            if (annotation is MKUserLocation)
            {
                return null;
            }

            var customPin = GetCustomPin(annotation as MKPointAnnotation);
            if (customPin == null)
            {
                throw new ArgumentException("Custom pin not found");
            }

            MKAnnotationView annotationView = mapView.DequeueReusableAnnotation(customPin.Name);
            if (annotationView == null)
            {
                annotationView = new CustomMKAnnotationView(annotation, customPin.Name)
                {
                    Image = UIImage.FromFile("pin.png"),
                    CalloutOffset = new CGPoint(0, 0)
                };

                ((CustomMKAnnotationView)annotationView).Name = customPin.Name;
                ((CustomMKAnnotationView)annotationView).Url = customPin.Url;
                ((CustomMKAnnotationView)annotationView).DealType = customPin.DealType;
            }

            annotationView.CanShowCallout = false;

            return annotationView;
        }

        /// <summary>
        /// The on element changed.
        /// </summary>
        /// <param name="e"> The element changed event. </param>
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null && Control is MKMapView nativeMap)
            {
                nativeMap.RemoveAnnotations(nativeMap.Annotations);
                nativeMap.GetViewForAnnotation = null;

                nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
                nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
            }

            if (e.NewElement != null)
            {
                var formsMap = (MobileNativeMap)e.NewElement;
                _customPins = formsMap.NPCompletPins;

                if (!(Control is MKMapView mkMapView))
                {
                    return;
                }

                mkMapView.GetViewForAnnotation = GetViewForAnnotation;
                mkMapView.DidSelectAnnotationView += OnDidSelectAnnotationView;
                mkMapView.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
            }
        }

        private NPCompletPin GetCustomPin(MKPointAnnotation annotation)
        {
            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            return _customPins.FirstOrDefault(pin => pin.Position == position);
        }

        private void OnDidDeselectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            if (e.View.Selected)
            {
                return;
            }

            _customPinView.RemoveFromSuperview();
            _customPinView.Dispose();
            _customPinView = null;
        }

        private void OnDidSelectAnnotationView(object sender, MKAnnotationViewEventArgs e)
        {
            _customPinView = new UIView();
        }
    }
}