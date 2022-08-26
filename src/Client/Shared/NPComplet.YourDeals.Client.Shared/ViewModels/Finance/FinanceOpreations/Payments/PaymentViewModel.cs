using Microsoft.AspNetCore.SignalR.Client;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.Logging;
using NPComplet.YourDeals.Client.Shared.ViewModels.Finance.FinanceOpreations.Payments.Interfaces;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.FinanceDTO;
using NPComplet.YourDeals.Domain.Shared.Finance;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceSupports;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using Radzen;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using NPComplet.YourDeals.Client.Shared.ViewModels.Base;
using MvvmBlazor.ViewModel;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Finance.FinanceOpreations.Payments
{
   public class PaymentViewModel: ViewModelBase
    {
        private readonly HttpClient _httpClient;

        private readonly HubConnection _hubConnection;
        private readonly NotificationService _notificationService;
      
      
        public CrossingPayment CrossingPayment { get; set; }
        public PostingDealPayment PostingDealPayment { get; set; }
        public FinancialOpretionDTO financialOpreationDto { get; set; }
        public List<CreditCard> financeSupports { get; set; } = new List<CreditCard>();

        public PaymentViewModel(HttpClient httpClient, NotificationService notificationService)
        {
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            this._notificationService = notificationService;
          
       
           
        }

        public async Task GetCrossingPaymentAsync(Guid Id)
        {
            var result = await this._httpClient.GetFromJsonAsync<Result<CrossingPayment>>(string.Format(Config.GetTransactionById,Id));

            CrossingPayment = result.Data;
        }

        public Task PostPaymentAsync()
        {
            throw new NotImplementedException();
        }

        public async Task GetFinancialSupportsAsync()
        {
            financeSupports = new List<CreditCard>();
            var result = await this._httpClient.GetFromJsonAsync<Result<List<CreditCard>>>(Config.GetUserCreditCards);

            financeSupports = result.Data;
        }

        

       

        public Task GetPostingDealPaymentAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task DeletePaymentMethod(Guid Id)
        {
            var result = await _httpClient.DeleteAsync(string.Format(Config.DeleteUserCreditCard, Id));
            if(result.IsSuccessStatusCode)
            {
                await GetFinancialSupportsAsync();
            }
        }
    }
}
