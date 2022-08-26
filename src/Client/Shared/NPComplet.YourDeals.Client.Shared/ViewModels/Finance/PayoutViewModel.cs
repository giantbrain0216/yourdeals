using Microsoft.AspNetCore.SignalR.Client;
using NPComplet.YourDeals.Client.Shared.Logging;
using NPComplet.YourDeals.Client.Shared.WebUI.Pages.Finance.FinanceSupportsUI;
using NPComplet.YourDeals.Domain.Shared.Finance;
using Radzen;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NPComplet.YourDeals.Client.Shared.Services;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Finance
{
    public class PayoutViewModel : Prism.Mvvm.BindableBase, IDisposable
    {
        private bool _isAllAmount;

        public bool isAllAmount { get { return _isAllAmount; } set { SetProperty(ref _isAllAmount, value); } }


        public decimal availableamount { get; set; }

        public List<FinanceSupport> financeSupports { get; set; }

        public CreditCardUI cerditCardUI;

        public decimal anotheramount { get; set; }

        private readonly DialogService _dialogService;

        private readonly HttpClient _httpClient;

        private readonly HubConnection _hubConnection;

        private readonly NotificationService _notificationService;
   
       
        public PayoutViewModel(IHttpClientService httpClientService, DialogService dialogService, NotificationService notificationService)
        {
            this._httpClient = httpClientService.ApplicationHttpClient ?? throw new ArgumentNullException(nameof(httpClientService.ApplicationHttpClient));
            this._dialogService = dialogService;
            this._notificationService = notificationService;
      
        
      
        }
        public async Task GetUserFinanceSupport()
        {

        }
        public async Task FormSubmit()
        {
           

        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
