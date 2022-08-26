// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapVM.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Client.Shared.Services.BackgroudServices;
using NPComplet.YourDeals.Domain.Shared.Constant.Application;
using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
using NPComplet.YourDeals.Domain.Shared.Deal.Requests;
using NPComplet.YourDeals.Domain.Shared.Search;
using Prism.Mvvm;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Deals
{
    #region

    #endregion

    /// <summary>
    ///     The map vm.
    /// </summary>
    public class MapViewModel : BindableBase, IDisposable
    {
        private readonly HttpClient _httpClient;

        private readonly HubConnection _hubConnection;
 

        private IList<Offer> _offerPins;

        private IList<Request> _requestPins;

        private List<Tag> _tags;
#pragma warning disable S2933 // Fields that are only assigned in the constructor should be "readonly"
        private HttpClientBackgroudService httpClientBackgroundService;
#pragma warning restore S2933 // Fields that are only assigned in the constructor should be "readonly"

#pragma warning disable S2933 // Fields that are only assigned in the constructor should be "readonly"
        private RefrechUserTokenBackgroudServices RefrechUser;
#pragma warning restore S2933 // Fields that are only assigned in the constructor should be "readonly"
        /// <summary>
        /// Initializes a new instance of the <see cref="MapViewModel"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        public MapViewModel(HttpClientBackgroudService wsamservice, RefrechUserTokenBackgroudServices refrechUserTokenBackgroudServices, IHttpClientService httpClientService)
        {
#pragma warning disable S3928 // Parameter names used into ArgumentException constructors should match an existing one 
            this._httpClient = httpClientService.ApplicationHttpClient ?? throw new ArgumentNullException(nameof(IHttpClientService));
#pragma warning restore S3928 // Parameter names used into ArgumentException constructors should match an existing one 

            this.OfferPins = new List<Offer>();
            this.RequestPins = new List<Request>();
            this.Tags = new List<Tag>();
            httpClientBackgroundService = wsamservice;
            RefrechUser = refrechUserTokenBackgroudServices;
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            RunBackgroundService();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            this._hubConnection = new HubConnectionBuilder()
                .WithUrl(Config.BaseUrl + ApplicationConstants.SignalR.DealHubUrl, options => { })
                .WithAutomaticReconnect().Build();
            if (this._hubConnection.State == HubConnectionState.Disconnected) Task.Run(this.StartHub);

            this._hubConnection.On<List<Offer>>(
                ApplicationConstants.SignalR.RealTimeOffer,
                async chatHistory => { await this.LoadData(); });

            Console.WriteLine("MapViewModel");
        }

        public async Task RunBackgroundService()
        {
            await httpClientBackgroundService.StartWorking();
          
        }
        /// <summary>
        ///     Gets or sets a value indicating whether is search.
        /// </summary>
        public bool IsSearch { get; set; }

        /// <summary>
        ///     Gets or sets the offer pins.
        /// </summary>
        public IList<Offer> OfferPins
        {
            get => this._offerPins;
            set => this.SetProperty(ref this._offerPins, value);
        }

        /// <summary>
        ///     Gets or sets the request pins.
        /// </summary>
        public IList<Request> RequestPins
        {
            get => this._requestPins;
            set => this.SetProperty(ref this._requestPins, value);
        }

        /// <summary>
        ///     Gets or sets the selected tag.
        /// </summary>
        public Tag SelectedTag { get; set; }

        /// <summary>
        ///     Gets or sets the tags.
        /// </summary>
        public List<Tag> Tags
        {
            get => this._tags;
            set => this.SetProperty(ref this._tags, value);
        }

        /// <summary>
        /// The get nearest offer.
        /// </summary>
        /// <param name="latitude">
        /// The latitude.
        /// </param>
        /// <param name="longitude">
        /// The longitude.
        /// </param>
        /// <param name="zoom">
        /// The zoom.
        /// </param>
        /// <param name="orderBy">
        /// The order by.
        /// </param>
        /// <param name="include">
        /// The include.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IList<Offer>> GetNearestOffer(
            double latitude,
            double longitude,
            double zoom,
            string orderBy,
            string include)
        {

            await RefrechUser.StartWorking();
            return await this._httpClient.GetFromJsonAsync<Offer[]>(Config.GetAllOffers);
        }

        /// <summary>
        /// The get nearest request.
        /// </summary>
        /// <param name="latitude">
        /// The latitude.
        /// </param>
        /// <param name="longitude">
        /// The longitude.
        /// </param>
        /// <param name="zoom">
        /// The zoom.
        /// </param>
        /// <param name="orderBy">
        /// The order by.
        /// </param>
        /// <param name="include">
        /// The include.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IList<Request>> GetNearestRequest(
            double latitude,
            double longitude,
            double zoom,
            string orderBy,
            string include)
        {
           

            return await this._httpClient.GetFromJsonAsync<Request[]>(Config.GetAllRequests);
        }

        /// <summary>
        ///     The load data.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public async Task LoadData()
        {
            this.OfferPins = await this.GetNearestOffer(48.9566f, 2.3522f, 13, "ID", "Address%20Address.Location");
            this.RequestPins = await this.GetNearestRequest(48.9566f, 2.3522f, 13, "ID", "Address%20Address.Location");

            foreach (var offerPin in this.OfferPins) this.ManageSearchTags(offerPin.SearchTags);

            foreach (var requestPin in this.RequestPins) this.ManageSearchTags(requestPin.SearchTags);
        }

        private void ManageSearchTags(ICollection<Tag> searchTags)
        {
            if (searchTags == null || !searchTags.Any()) return;

            foreach (var tag in searchTags)
            {
                var isAvailable = this.Tags.FirstOrDefault(p => p.TagString.Contains(tag.TagString));
                if (isAvailable == null) this.Tags.Add(tag);
            }
        }

        private async Task StartHub()
        {
            await this._hubConnection.StartAsync();
        }
         void IDisposable.Dispose()
        {
            // Method intentionally left empty.
        }
    }
}