// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealPageViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AntDesign;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using MvvmBlazor.ViewModel;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Client.Shared.WebUI.Pages.Deals.DealPage;
using NPComplet.YourDeals.Client.Shared.WebUI.Pages.UserPages.Communication;
using NPComplet.YourDeals.Domain.Shared.Constant.Application;
using NPComplet.YourDeals.Domain.Shared.Deal;
using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
using NPComplet.YourDeals.Domain.Shared.Deal.Requests;
using Radzen;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Client.Shared.Extensions;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Deals
{
    

    /// <summary>
    ///     The the deal view model.
    /// </summary>
    public class DealPageViewModel : ViewModelBase
    {
        private readonly HttpClient _httpClient;
        private Deal _deal;
        private readonly ILogger<DealPageViewModel> _logger;
        #region fields
        // private string OwnerBase64 { get; set; }
        private string _ownerBase64;

        public string OwnerBase64
        {
            get { return _ownerBase64; }
            set { _ownerBase64 = value;
            
            }
        }


        private Carousel _carousel;

        public Carousel Carousel
        {
            get { return _carousel; }
            set { _carousel = value;
            
            }
        }

        //public string Id { get; set; }
        private string _id;
        [Parameter]
        public string Id
        {
            get { return _id; }
            set { _id = value;
            
            }
        }

        
        //public string Type { get; set; }
        private string _type;
        [Parameter]
        public string Type
        {
            get { return _type; }
            set { _type = value;
            
            }
        }

        
        public DialogService _dialogService { get; set; }
        public bool _visible;
        

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value;
             
            }
        }

        public bool _isOwner;
        

        public bool IsOwner
        {
            get { return _isOwner; }
            set { _isOwner = value;
            
            }
        }

        public bool _isOffer;
        

        public bool IsOffer
        {
            get { return _isOffer; }
            set { _isOffer = value;
            
            }
        }

        
        private bool _showMakeProposition =false;

        public bool ShowMakeProposition
        {
            get { return _showMakeProposition; }
            set { _showMakeProposition = value;
            
            }
        }
        private bool _isOnline;

        public bool IsOnline
        {
            get { return _isOnline; }
            set { _isOnline = value; 
           
            }
        }

        private IList<string> _imagesPath;

        public IList<string> ImagesPath
        {
            get { return _imagesPath; }
            set { _imagesPath = value; }
        }

        
        private string _warranty;

        public string Warranty
        {
            get { return _warranty; }
            set { _warranty = value;
            
            }
        }

        
        private string _termAndCondition;

        public string TermAndCondition
        {
            get { return _termAndCondition; }
            set { _termAndCondition = value; 
            
            }
        }

        
        private string _userId;

        public string UserId
        {
            get { return _userId; }
            set { _userId = value;
                
            }
        }

        
        private bool _isloading;

        public bool Isloading
        {
            get { return _isloading; }
            set { _isloading = value; 
            
            }
        }

        
        private IList<PropositionOffer> _propositionOffers;

        public IList<PropositionOffer> PropositionOffers
        {
            get { return _propositionOffers; }
            set { _propositionOffers = value; }
        }


        
        private IList<PropositionRequest> _propositionRequests;

        public IList<PropositionRequest> PropositionRequests
        {
            get { return _propositionRequests; }
            set { _propositionRequests = value; 
            
            }
        }


        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="DealPageViewModel"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        /// <param name="logger">The logger.</param>
        public DealPageViewModel(IHttpClientService httpInterceptorService, ILogger<DealPageViewModel> logger,DialogService dialogService)
        {
            _httpClient = httpInterceptorService.ApplicationHttpClient;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dialogService = dialogService;
        }

        /// <inheritdoc />
        private Deal deal;

        public Deal Deal
        {
            get { return deal; }
            set { deal = value;
                
            }
        }


        /// <inheritdoc />
        public async Task<Deal> GetRequestDealById(string id)
        {
           

            try
            {
                return await _httpClient.GetFromJsonAsync<Request>($"{Config.GetRequestById}{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }


            return null;
        }

        /// <inheritdoc />
        public async Task<Offer> GetOfferDealById(string id)
        {
           

            try
            {
                return await _httpClient.GetFromJsonAsync<Offer>($"{Config.GetOfferById}{id}");
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
            }

        

            return null;
        }

        /// <inheritdoc />
        public async Task<IList<T>> GetPropositions<T>(string id, string userId, bool isOwner, bool isOffers = false)
        {
            try
            {
                if (isOwner)
                {
                    return await _httpClient.GetFromJsonAsync<IList<T>>(string.Format(isOffers ? Config.PropositionOffers : Config.PropositionRequests, id));
                }

                return await _httpClient.GetFromJsonAsync<IList<T>>(string.Format(isOffers ? Config.PropositionOffersByOwner : Config.PropositionRequestsByOwner, id, userId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        public EventCallback CreatePropositionCommand => new EventCallback(null, (Action<object>)(async (object msg) =>
        {
           await OpenCreatePropositionAsync();
        }));

        public HubConnection HubConnection { get; set; }
    

        

        public override async  Task OnInitializedAsync()
        {
            try
            {
                UserId = NPCompletApp.ShellViewModel.UserState.User.GetUserId();
                Isloading = true;
               //UserId = NPCompletApp.ShellViewModel.UserState?.User?.GetUserId();

                IsOffer = Type == "offer";
                if (IsOffer)
                {
                    var offer = await GetOfferDealById(Id);
                    Deal = offer;
                    Warranty = offer.Quotation?.WarrantyPost?.Description ?? string.Empty;
                    TermAndCondition = offer.Quotation?.TermConditionPost?.Description ?? string.Empty;
                }
                else
                {
                    Deal = await GetRequestDealById(Id);
                }

                ImagesPath = Deal.DealDocument?.DealFiles?.Select(f => f.FileName).ToList() ?? new List<string>();
                IsOwner = Deal.OwnerId?.ToString() == _userId;

                await FillPropositionsAsync();

                OwnerBase64 = convertImgToBase64(Deal.Owner);

                Isloading = false;
                try
                {
                    HubConnection = new HubConnectionBuilder().WithUrl(Config.BaseUrl + ApplicationConstants.SignalR.HubUrl, options =>
                    {
                        // options.SkipNegotiation = true;

                        options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling;
                        options.Headers.Add("Authorization", $"Bearer {NPCompletApp.Token}");
                        options.AccessTokenProvider = async () => await Task.FromResult(NPCompletApp.Token);
                    }).WithAutomaticReconnect().Build();


                    if (HubConnection.State == HubConnectionState.Disconnected)
                    {
                        await HubConnection.StartAsync();
                    }

                    HubConnection.On<string>(ApplicationConstants.SignalR.PingResponse, async (userId) =>
                    {
                        await handlePingResponse(userId);
                    });
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.PingRequest, NPCompletApp.ShellViewModel.UserState.User.GetUserId());
                }
                catch (Exception)
                {

                    throw;
                }

          
            }
            catch(Exception ex)
            {
                throw;
            }
          
        }

        public override  async Task OnParametersSetAsync()
        {
         
            
        }

        public async Task handlePingResponse(string userId)
        {
            if (userId == Deal.Owner.Id.ToString())
            {
                IsOnline = true;
            }
            StateHasChanged();
        }

       public async Task OpenChatAsync(string cid, Guid? roomId)
        {
            await _dialogService.OpenAsync<Chat>(
                "",
                new Dictionary<string, object>
                {
                {"cid", cid},
                {"Style", "width:100% !important; height: 100% !important"},
                {"RoomId", roomId},
                    {"CurrentUserBase64",OwnerBase64 }
},
                new Radzen.DialogOptions { Width = "750px", Height = "500px", Resizable = true, Draggable = true });
        }

       public async Task OpenCreatePropositionAsync()
        {

            var id = Id;
            Guid.TryParse(Id, out Guid guidResult);
            await _dialogService.OpenAsync<CreateProposition>(
                "",
                new Dictionary<string, object>
                {
                {"Id", guidResult},
                {"IsOffer", IsOffer},
                {"RefreshComponent", RefreshComponent},
                {"DealMessagePostId", Deal.DealMessagesPostId},
                {"Warranty", Warranty},
                {"TermAndCondition", TermAndCondition},
                {"PaymentManors", Deal.DealPriceReference.PaymentManors.Select(pm=>pm.PaymentManor).ToList()},
                {"OwnerId",Deal?.OwnerId },
                {"ProposerId",NPCompletApp.ShellViewModel.UserState.User.GetUserId()}
},
                new Radzen.DialogOptions { Width = IsOffer ? "750px" : "800px", Height = IsOffer ? "500px" : "530", Resizable = true, Draggable = true });
        }

        public EventCallback RefreshComponent => new EventCallback(null, (Action)(async () =>
        {
            await FillPropositionsAsync();
           StateHasChanged();
        }));

        public async Task FillPropositionsAsync()
        {
            Isloading = true;
           
            if (string.IsNullOrEmpty(UserId))
            {
                return;
            }

            if (IsOffer)
            {
                PropositionOffers = await GetPropositions<PropositionOffer>(Id, UserId, IsOwner, true);
            }
            else
            {
                PropositionRequests = await GetPropositions<PropositionRequest>(Id, UserId, IsOwner);
            }

            ShowMakeProposition = !IsOwner && (PropositionOffers == null || !PropositionOffers.Any()) && (PropositionRequests == null || !PropositionRequests.Any());
            Isloading = false;
            StateHasChanged();
        }

        public async Task OnOfferChange(bool args, PropositionOffer propositionOffer)
        {
            if (!args)
                return;

            await _dialogService.OpenAsync<AcceptOrRejectProposition>(
                "",
                new Dictionary<string, object>
                {
                {"Id", propositionOffer.Id},
                {"IsOffer", true},
                {"RefreshComponent", RefreshComponent},
                {"TotalAmount", propositionOffer.TotalAmount},
                {"Title", propositionOffer.PropositionMessagesPost.Title}
},
                new Radzen.DialogOptions { Width = "435px", Height = "200px", Resizable = true, Draggable = true });
        }

        public async Task OnRequestChange(bool args, PropositionRequest propositionRequest)
        {
            if (!args)
                return;

            await _dialogService.OpenAsync<AcceptOrRejectProposition>(
                "",
                new Dictionary<string, object>
                {
                {"Id", propositionRequest.Id},
                {"IsOffer", false},
                {"RefreshComponent", RefreshComponent},
                {"TotalAmount", propositionRequest.TotalAmount},
                {"Title", propositionRequest.PropositionMessagesPost.Title},
                {"Warranty", propositionRequest.Quotation?.WarrantyPost?.Description ?? string.Empty},
                {"TermAndCondition", propositionRequest.Quotation?.TermConditionPost?.Description},
                {"OwnerId",propositionRequest?.OwnerId },
                {"ProposerId",NPCompletApp.ShellViewModel.UserState.User.GetUserId()},
                
                 
},
                
                new Radzen.DialogOptions { Width = "435px", Height = "250px", Resizable = true, Draggable = true });; ;
        }




        public string convertImgToBase64(User user)
        {
            string Base64 =
            "data:image/"
                    + $"{user.Profile.ProfileImage.FileExtension};"
                    + "base64, "
                    + Convert.ToBase64String(user.Profile.ProfileImage.Data);

            return Base64;
        }
    }
}