// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateDealVM.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmBlazor.ViewModel;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.Logging;
using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Client.Shared.ViewModels.Base;
using NPComplet.YourDeals.Client.Shared.ViewModels.Deals.Interfaces;
using NPComplet.YourDeals.Client.Shared.WebUI.Pages.LocationsUI;
using NPComplet.YourDeals.Client.Shared.WebUI.Shared;
using NPComplet.YourDeals.Domain.Shared.Accounting;
using NPComplet.YourDeals.Domain.Shared.Communication;
using NPComplet.YourDeals.Domain.Shared.Constant.Application;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto;
using NPComplet.YourDeals.Domain.Shared.Deal;
using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
using NPComplet.YourDeals.Domain.Shared.Deal.Requests;
using NPComplet.YourDeals.Domain.Shared.Document;
using NPComplet.YourDeals.Domain.Shared.Pricing;
using NPComplet.YourDeals.Domain.Shared.Search;
using NPComplet.YourDeals.Domain.Shared.Shared;
using Radzen;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Deals
{
    #region

    #endregion

    /// <summary>
    ///     The create deal vm.
    /// </summary>
    public class CreateDealViewModel : ViewModelBase
    {
        private readonly DialogService _dialogService;

        private readonly HttpClient _httpClient;

        private readonly HubConnection _hubConnection;

        private readonly NotificationService _notificationService;
        public IHttpClientService httpClientService { get; set; }
        NavigationManager _navMan;
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDealViewModel"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        /// <param name="dialogService">
        /// The dialog service.
        /// </param>
        /// <param name="notificationService">
        /// The notification service.
        /// </param>
        public CreateDealViewModel(DialogService dialogService, NotificationService notificationService, IHttpClientService httpInterceptorService, NavigationManager navMan)
        {
            this._httpClient = httpInterceptorService.ApplicationHttpClient ?? throw new ArgumentNullException(nameof(IHttpClientService));
            this._dialogService = dialogService;
            this._notificationService = notificationService;
          
            _navMan = navMan;
            this.Location = new Location();

            this.Token = NPCompletApp.Token;

            this._hubConnection = new HubConnectionBuilder()
                .WithUrl(Config.BaseUrl + ApplicationConstants.SignalR.DealHubUrl, options => { })
                .WithAutomaticReconnect().Build();
            if (this._hubConnection.State == HubConnectionState.Disconnected) Task.Run(this.StartHub);

        }


        //public bool isMobile { get; set; } = false;
        private bool _isMobile;
        [Parameter]
        public bool isMobile
        {
            get { return _isMobile; }
            set { _isMobile = value;
            OnPropertyChanged(nameof(isMobile));
            }
        }


        
        //public string laitude { get; set; }
        private string _laitude;
        [Parameter]
        public string laitude
        {
            get { return _laitude; }
            set { _laitude = value;
            OnPropertyChanged(nameof(laitude));
            }
        }
        
        //public string longtude { get; set; }
        private string _longtude;
        [Parameter]
        public string longtude
        {
            get { return _longtude; }
            set { _longtude = value;
            OnPropertyChanged(nameof(longtude));
            }
        }

        
        //public float Radius { get; set; }
        private float _radius;
        [Parameter]
        public float Radius
        {
            get { return _radius; }
            set { _radius = value; 
            OnPropertyChanged(nameof(Radius));
            }
        }


        
        //public EventCallback<bool> OnSubmit { get; set; }
        private EventCallback<bool> _onSubmit;
        [Parameter]
        public EventCallback<bool> OnSubmit
        {
            get { return _onSubmit; }
            set { _onSubmit = value; }
        }


        /// <summary>
        /// Gets or sets the image upload.
        /// </summary>
        public ImageUpload ImageUpload { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is offer.
        /// </summary>
        public bool IsOffer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether loading.
        /// </summary>
        public bool Loading { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Gets or sets the offer.
        /// </summary>
        public OfferDealViewModel Offer { get; set; } = new OfferDealViewModel();

        /// <summary>
        /// Gets or sets the request.
        /// </summary>
        public DealViewModel Request { get; set; } = new DealViewModel();

        /// <summary>
        /// Gets or sets the set position.
        /// </summary>
        public SetPostion SetPosition { get; set; }

        /// <summary>
        /// Gets or sets the tag builder control.
        /// </summary>
        public TagBuilderControl TagBuilderControl { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The deal offer.
        /// </summary>
        public void DealOffer()
        {
            this.IsOffer = true;
        }

        /// <summary>
        /// The deal request.
        /// </summary>
        public void DealRequest()
        {
            this.IsOffer = false;
        }

        /// <summary>
        /// The handle smart tagging.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task HandleSmartTagging()
        {
            if (string.IsNullOrEmpty(Offer.Description) && string.IsNullOrEmpty(Request.Description))
            {
                return;
            }

            NPCompletApp.ShellViewModel.isBusy = true;

            var result = await this._httpClient.GetFromJsonAsync<List<NlpTagItem>>(
                             Config.GetTags + (this.IsOffer ? this.Offer.Description : this.Request.Description));

            if (result == null) return;

            foreach (var item in result)
            {
                foreach (var word in item.TaggedWords)
                {
                    if (!word.Tag.Contains("NN")) 
                        continue;
                    if (string.IsNullOrEmpty(this.TagBuilderControl.Tags.FirstOrDefault(p => p.Contains(word.Word)))) 
                        this.TagBuilderControl.Tags.Add(word.Word);
                }
            }

            this.TagBuilderControl.statechanged();

            NPCompletApp.ShellViewModel.isBusy = false;
        }

        /// <summary>
        /// The handle submit.
        /// </summary>
        /// <param name="onSubmit">
        /// The on submit.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task HandleSubmit(EventCallback<bool> onSubmit)
        {
            this.Loading = true;
            NPCompletApp.ShellViewModel.isBusy = true;

            if (this.IsOffer) 
                await this.CreateOfferDealAsync(onSubmit);
            else 
                await this.CreateRequestDealAsync(onSubmit);

            this.Loading = false;
        }

        /// <summary>
        /// The open set pos screen.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void OpenSetPosScreen()
        {
            throw new NotImplementedException();
        }

        private Deal CreateInitialDeal()
        {
            var deal = new Deal
            {
                Address = new Address
                {
                    Location = new Location
                    {
                        Latitude = this.SetPosition._selectedMapResponce.Lat,
                        Longitude = this.SetPosition._selectedMapResponce.Lon,
                        Radius = this.SetPosition._selectedMapResponce.radius
                    }
                },
                DealDocument = new DealDocument
                {
                    DealFiles = new List<StoredFile>()
                },
                SearchTags = new List<Tag>(),
                DealPriceReference = new DealPriceReference(),
                DealMessagesPost = new DealMessagesPost()
            };

            foreach (var storedFile in this.ImageUpload.ImgUrl.Select(imgUrl => new StoredFile { FileName = imgUrl }))
            {
                deal.DealDocument.DealFiles.Add(storedFile);
            }

            foreach (var tag in this.TagBuilderControl.Tags.Select(tag => new Tag { TagString = tag }))
            {
                deal.SearchTags.Add(tag);
            }

            return deal;
        }

        private async Task CreateOfferDealAsync(EventCallback<bool> onSubmit)
        {
            var offer = new Offer
            {
                Address = new Address
                {
                    Location = new Location
                    {
                        Latitude = this.SetPosition._selectedMapResponce.Lat,
                        Longitude = this.SetPosition._selectedMapResponce.Lon,
                        Radius = this.SetPosition._selectedMapResponce.radius
                    }
                },
                DealDocument = new DealDocument
                {
                    DealFiles = new List<StoredFile>()
                },
                SearchTags = new List<Tag>(),
                DealPriceReference = new DealPriceReference(),
                DealMessagesPost = new DealMessagesPost()
            };

            foreach (var storedFile in this.ImageUpload.ImgUrl.Select(imgUrl => new StoredFile { FileName = imgUrl }))
            {
                offer.DealDocument.DealFiles.Add(storedFile);
            }

            foreach (var tag in this.TagBuilderControl.Tags.Select(tag => new Tag { TagString = tag }))
            {
                offer.SearchTags.Add(tag);
            }
            offer.DealDocument.Description = this.Offer.Description;
            offer.DealDocument.Title = this.Offer.Title;
            offer.DealPriceReference.AmountValue = this.Offer.Price;

            offer.DealPriceReference.PaymentManors = Offer.PaymentManors.Select(pm => new DealPaymentManor { PaymentManor = pm }).ToList();

            offer.Quotation = new Quotation
            {
                TermConditionPost = new Post
                {
                    Description = Offer.TermConditionDescription,
                },
                WarrantyPost = new Post
                {
                    Description = Offer.WarrantyDescription
                }
            };

            var result = await this._httpClient.PostAsJsonAsync(Config.CreateOffer, offer);
            NPCompletApp.ShellViewModel.isBusy = false;

            if (result.IsSuccessStatusCode)
            {
                this._notificationService.Notify(
                    new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Duration = 1000,
                        Summary = "Sucess",
                        Detail = "Deal Created Successfully"
                    });

                var offers = new List<Offer>();
                await this._hubConnection.SendAsync(ApplicationConstants.SignalR.RealTimeOffer, offers);

                await onSubmit.InvokeAsync(true);
                this._dialogService.Close();
            }
        }

        private async Task CreateRequestDealAsync(EventCallback<bool> onSubmit)
        {
            var deal = this.CreateInitialDeal();

            deal.DealDocument.Description = this.Request.Description;
            deal.DealDocument.Title = this.Request.Title;
            deal.DealPriceReference.AmountValue = this.Request.Price;

            deal.DealPriceReference.PaymentManors = Request.PaymentManors.Select(pm => new DealPaymentManor { PaymentManor = pm }).ToList();

            var result = await this._httpClient.PostAsJsonAsync(Config.CreateRequest, deal);
            NPCompletApp.ShellViewModel.isBusy = false;

            if (result.IsSuccessStatusCode)
            {
                this._notificationService.Notify(
                    new NotificationMessage
                    {
                        Severity = NotificationSeverity.Success,
                        Duration = 1000,
                        Summary = "Sucess",
                        Detail = "Deal Created Successfully"
                    });

                var requests = new List<Request>();
                await this._hubConnection.SendAsync(ApplicationConstants.SignalR.RealTimeRequest, requests);

                await onSubmit.InvokeAsync(true);
                this._dialogService.Close();
            }
        }

        private async Task StartHub()
        {
            await this._hubConnection.StartAsync();
        }



        public override async Task OnInitializedAsync()
        {
            await NPCompletApp.ShellViewModel.GetUserStatus();
            if (!NPCompletApp.ShellViewModel.isLoggedIn)
            {
                this._navMan.NavigateTo("/login");
            }
        }

       public void set()
        {
        }

        public void AssignImageUrl(string[] imgUrl)
        {
        }
    }
}