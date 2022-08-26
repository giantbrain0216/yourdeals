// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MobileCurrentLocationViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Prism.Mvvm;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Shared
{
    #region

    #endregion

    /// <summary>
    ///     The mobile current location view model.
    /// </summary>
    public class MobileCurrentLocationViewModel : BindableBase
    {
        private MapSpan _mapSpan;

        /// <summary>
        ///     Gets or sets the map span.
        /// </summary>
        public MapSpan MapSpan
        {
            get => this._mapSpan;
            set => this.SetProperty(ref this._mapSpan, value);
        }

        /// <summary>
        ///     The get current location.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public async Task GetCurrentLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                this.MapSpan = MapSpan.FromCenterAndRadius(
                    location != null
                        ? new Position(location.Latitude, location.Longitude)
                        : new Position(48.9566f, 2.3522f),
                    Distance.FromMiles(12d));
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException)
            {
                // Handle permission exception
            }
            catch (Exception)
            {
                // Unable to get location
            }
        }
    }
}