using Microsoft.AspNetCore.SignalR.Client;
using NPComplet.YourDeals.Client.Shared.Logging;
using NPComplet.YourDeals.Domain.Shared.Enumerable;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceSupports;
using Radzen;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Client.Shared.Helpers;
using System.Net.Http.Json;
using AntDesign;
using NPComplet.YourDeals.Client.Shared.ViewModels.Base;
using NotificationService = Radzen.NotificationService;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Finance.FinanceSupports
{
   public class FinanceSupportViewModel : BaseViewModel
    {
        public bool formDisabled { get; set; }
        public string message = "";
        public CreditCard creditCard { get; set; } = new CreditCard();
        private readonly DialogService _dialogService;

        private readonly HttpClient _httpClient;

        private readonly HubConnection _hubConnection;

        private readonly NotificationService _notificationService;

        private readonly DrawerService _drawerService;
        public DrawerRef<string> drawerRef { get; set; }

        public bool isEdit { get; set; }
        public FinanceSupportViewModel(IHttpClientService httpClientService, DialogService dialogService, NotificationService notificationService, DrawerService drawerService)
        {
            this._httpClient = httpClientService.ApplicationHttpClient ?? throw new ArgumentNullException(nameof(httpClientService.ApplicationHttpClient));
            this._dialogService = dialogService;
            this._notificationService = notificationService;
            _drawerService = drawerService;
    
        }
            public async Task FormSubmit()
        {
            formDisabled = true;
            creditCard.Number = creditCard.Number.Replace(" ", "");
            creditCard.Type = FindType(creditCard.Number);
            var date = creditCard.ExpirationDate.Split("/");
            creditCard.ExpireMonth = int.Parse(date[0]);
            creditCard.ExpireYear = int.Parse(date[1]);
            HttpResponseMessage result;
            if (!isEdit)
            {
                result = await _httpClient.PostAsJsonAsync<CreditCard>(Config.SaveUserCreditCard, creditCard);
            }
            else
            {
                 result = await _httpClient.PutAsJsonAsync<CreditCard>(string.Format(Config.UpdateUserCreditCard,creditCard.Id), creditCard);
            }
            formDisabled = false;
            if (result.IsSuccessStatusCode)
            {
                await drawerRef!.CloseAsync("");
            }

            else
            {
                message = result.ReasonPhrase;
            }
        }

        public static CreditCardType FindType(string cardNumber)
        {
            //https://www.regular-expressions.info/creditcard.html
            if (Regex.Match(cardNumber, @"^4[0-9]{12}(?:[0-9]{3})?$").Success)
            {
                return CreditCardType.Visa;
            }

            if (Regex.Match(cardNumber, @"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$").Success)
            {
                return CreditCardType.Mastercard;
            }

            if (Regex.Match(cardNumber, @"^3[47][0-9]{13}$").Success)
            {
                return CreditCardType.Amex;
            }

            if (Regex.Match(cardNumber, @"^6(?:011|5[0-9]{2})[0-9]{12}$").Success)
            {
                return CreditCardType.Discover;
            }

            return CreditCardType.Other;

           
        }

    }
}
