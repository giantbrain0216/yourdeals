using Microsoft.AspNetCore.SignalR.Client;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.Logging;
using NPComplet.YourDeals.Client.Shared.ViewModels.Finance.FinanceOpreations.Payments.Interfaces;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using NPComplet.YourDeals.Client.Shared.ViewModels.Base;
using MvvmBlazor.ViewModel;

using NPComplet.YourDeals.Client.Shared.DTOwithCallback;
using NPComplet.YourDeals.Domain.Shared.Enumerable;
using NPComplet.YourDeals.Client.Shared.WebUI.Pages.Accounts.Invoice;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Accounts.Invoice
{
    public class InvoiceViewModel : ViewModelBase
    {
        private readonly HttpClient _httpClient;

        private readonly HubConnection _hubConnection;
        private readonly NotificationService _notificationService;
        private AntDesign.DrawerService _drawerService;
        #region Propertise
        //int _pageIndex = 1;
        private int _pageIndex=1;

        public int PageIndex
        {
            get { return _pageIndex=1; }
            set { _pageIndex = value;
            OnPropertyChanged(nameof(PageIndex)); 
            }
        }
        private int _pageSize=10;

        public int PageSize
        {
            get { return _pageSize=10; }
            set { _pageSize = value; 
            OnPropertyChanged(nameof(PageSize));
            }
        }
        private int _total;

        public int Total
        {
            get { return _total; }
            set { _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }
        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            set { _loading = value;
                OnPropertyChanged(nameof(Loading));
            }
        }


        //int _pageSize = 10;
        //int _total = 0;
        //bool loading = false;
        #endregion

        public IList<Domain.Shared.Accounting.Invoice> UserInvoices { get; set; } = new List<Domain.Shared.Accounting.Invoice>();

        public InvoiceViewModel(HttpClient httpClient, NotificationService notificationService, AntDesign.DrawerService drawerService)
        {
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
          
            this._notificationService = notificationService;
          _drawerService = drawerService;
         
          
        }
        public async Task<int> GetTransactionsCountAsync()
        {
          

                return await _httpClient.GetFromJsonAsync<int>(Config.GetDealPaymentsCount);
           
        }
        public async Task GetUserInvoicesAsync(int pageindex,int size)
        {
          
            //var ListPaymentsResponse = await _httpClient.GetFromJsonAsync<Result<IEnumerable<Domain.Shared.Accounting.Invoice>>>(string.Format(Config.GetUserInvoices,pageindex,size));
            string url = string.Format(Config.GetUserInvoices, pageindex, size);
            var ListPaymentsResponse = await _httpClient.GetFromJsonAsync<Result<IEnumerable<Domain.Shared.Accounting.Invoice>>>(url);
            if (ListPaymentsResponse.Succeeded)
            {
               
                if (ListPaymentsResponse.Data.Any())
                {
                    UserInvoices = new List<Domain.Shared.Accounting.Invoice>(ListPaymentsResponse.Data);
                    Total = ListPaymentsResponse.Data.Count();
                    //foreach (var userInvoices in ListPaymentsResponse.Data)
                    //{

                    //    UserInvoices.Add(userInvoices);
                    //};
                }
            }
        }

       
        public override async Task OnInitializedAsync()
        {

            Loading = true;
            
            //Total = await GetTransactionsCountAsync();
            await GetUserInvoicesAsync(0, PageSize);
            StateHasChanged();
            Loading = false;

            await base.OnInitializedAsync();
        }
        public async Task PageIndexChanged(LoadDataArgs args)
        {
            Loading = true;
            await GetUserInvoicesAsync((int)(args.Skip != null ? args.Skip : 0), args.Top.Value);

            Loading = false;
            StateHasChanged();
        }

        //public async Task dopayment(CrossingPayment crosspayment)
        //{
        //    var options = new AntDesign.DrawerOptions()
        //    {
        //        Title = "CheckOut",
        //        Width = 450,

        //    };
        //    FinancialOpretionDtoCallback value = new FinancialOpretionDtoCallback();
        //    value.CrossingPayment = crosspayment;
        //    value.FinanceOpreationTable = FinanceOpreationTable.CrossingPayment;
        //    //var drawerRef = await _drawerService.CreateAsync<DoPayment, FinancialOpretionDtoCallback, string>(options, value);

        //    //drawerRef.OnClosed = async result =>
        //    //{
        //    //    Console.WriteLine("OnAfterClosed:" + result);
        //    //    //if (result != null)
        //    //    //    value = result;
        //    //    StateHasChanged();
        //    //};

        //}
    }
}
