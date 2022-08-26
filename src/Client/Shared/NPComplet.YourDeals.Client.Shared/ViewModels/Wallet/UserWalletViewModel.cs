using Microsoft.AspNetCore.SignalR.Client;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.Logging;
using NPComplet.YourDeals.Domain.Shared.Finance.EarningsWallet;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using Radzen;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Client.Shared.ViewModels.Deals;
using MvvmBlazor.ViewModel;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Wallet
{
    public class UserWalletViewModel : ViewModelBase
    {
        private readonly DialogService _dialogService;

        private readonly HttpClient _httpClient;

        private readonly HubConnection _hubConnection;

        private readonly NotificationService _notificationService;
        public IHttpClientService Interceptor { get; set; }
      
        public UserEarningsWallet UserEarningsWallet { get; set; }
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
        public UserWalletViewModel( DialogService dialogService, NotificationService notificationService, IHttpClientService httpClientService)
        {
            this._httpClient = httpClientService.ApplicationHttpClient ?? throw new ArgumentNullException(nameof(httpClientService.ApplicationHttpClient));
            this._dialogService = dialogService;
            this._notificationService = notificationService;
        
       
     
        }

        public async Task GetUserBalanceState()
        {
            var userbalance=await _httpClient.GetFromJsonAsync<Result<UserEarningsWallet>>(Config.GetUserBalanceState);
            if(userbalance.Succeeded)
            {
                UserEarningsWallet = userbalance.Data;
            }
            else
            {
                //Fake Zero Value
                UserEarningsWallet = new UserEarningsWallet();
                UserEarningsWallet.AvailableBlance = decimal.Zero;
            }
        }
        public override async Task OnInitializedAsync()
        {
            await GetUserBalanceState();
        }
        public void Dispose()
        {
           
        }
    }
}