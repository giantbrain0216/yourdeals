using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MvvmBlazor.ViewModel;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.Logging;
using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Client.Shared.ViewModels.Base;
using NPComplet.YourDeals.Client.Shared.ViewModels.Deals.Interfaces;
using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
using NPComplet.YourDeals.Domain.Shared.Deal.Requests;
using Prism.Mvvm;
using Radzen;
using Radzen.Blazor;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Deals
{
   
    public class DealsHistoryViewModel : ViewModelBase
    {
        public RadzenDataList<Request> _dataList;
        //public const int PageSize = 4;
        private int _PageSize;

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value;
            OnPropertyChanged(nameof(PageSize)); 
            }
        }

        public int _offersCount;
        public IList<Offer> _offers;
        public int _requestCount;
        public IList<Request> _requests;
        
        private Guid _id;
        [Parameter]
        public Guid Id
        {
            get { return _id; }
            set { _id = value;
            OnPropertyChanged(nameof(Id));
            }
        }



        private bool _isOffer;
        [Parameter]
        public bool IsOffer
        {
            get { return _isOffer; }
            set { _isOffer = value;
            OnPropertyChanged(nameof(IsOffer));
            }
        }


        public IList<PropositionOffer> _propositionOffers;
        public IList<PropositionRequest> _propositionRequests;
        private readonly HttpClient _httpClient;

        public IHttpClientService Interceptor { get; set; }


        public DealsHistoryViewModel(IHttpClientService httpClientService)
        {
            _httpClient = httpClientService.ApplicationHttpClient ?? throw new ArgumentNullException(nameof(httpClientService.ApplicationHttpClient));

            PageSize = 4;

        }

        /// <inheritdoc />
        public async Task<int> GetDealsCountAsync(bool isOffers = false)
        {
            try
            {
               
                return await _httpClient.GetFromJsonAsync<int>(isOffers ? Config.GetOffersCount : Config.GetRequestsCount);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IList<T>> GetDealsAsync<T>(int startIndex, int size, bool isOffers = false)
        {
            try
            {
                
              
                return await _httpClient.GetFromJsonAsync<IList<T>>(
                    string.Format(isOffers ? Config.GetOffersWithPagination : Config.GetRequestsWithPagination, startIndex, size));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IList<T>> GetSelectedPropositionsAsync<T>(Guid id, bool isOffers = false)
        {
            try
            {
       


                return await _httpClient.GetFromJsonAsync<IList<T>>(
                    string.Format(isOffers ? Config.SelectedPropositionOffers : Config.SelectedPropositionRequests, id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        public override Task OnAfterRenderAsync(bool firstRender)
        {
            _dataList = new RadzenDataList<Request>();
            return base.OnAfterRenderAsync(firstRender);
        }

        public override async Task OnInitializedAsync()
        {
            // load offers for the first tab.
            _offersCount = await GetDealsCountAsync(true);
            _offers = await GetDealsAsync<Offer>(0, PageSize, true);
            if (IsOffer)
            {
                _propositionOffers = await GetSelectedPropositionsAsync<PropositionOffer>(Id, true);
            }
            else
            {
                _propositionRequests = await GetSelectedPropositionsAsync<PropositionRequest>(Id);
            }
        }

       public async Task PageChangedAsync(PagerEventArgs args, bool isOffers)
        {
            if (isOffers)
            {
                _offers = await GetDealsAsync<Offer>(args.Skip, args.Top, isOffers);
            }
            else
            {
                _requests = await GetDealsAsync<Request>(args.Skip, args.Top);
            }
        }

        public async Task OnChange(int index)
        {
            //reload offers when tab changed
            if (index == 0)
            {
                _offersCount = await GetDealsCountAsync(true);
                _offers = await GetDealsAsync<Offer>(0, PageSize, true);
            }

            //reload requests when tab changed
            if (index == 1)
            {
                _requestCount = await GetDealsCountAsync();
                _requests = await GetDealsAsync<Request>(0, PageSize);
            }
        }
    }
}
