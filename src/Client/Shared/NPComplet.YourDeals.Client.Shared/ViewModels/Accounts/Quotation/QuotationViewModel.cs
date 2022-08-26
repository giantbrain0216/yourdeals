using Microsoft.AspNetCore.SignalR.Client;
using NPComplet.YourDeals.Client.Shared.Helpers;

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
using NPComplet.YourDeals.Client.Shared.WebUI.Pages.Accounts.Quotation;
using Microsoft.AspNetCore.Components;
using NPComplet.YourDeals.Domain.Shared.Deal.Requests;
using Microsoft.Extensions.Options;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using NPComplet.YourDeals.Client.Shared.Extensions;
using NPComplet.YourDeals.Client.Shared.WebUI.Pages.Deals.DealPage;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Accounts.Quotation
{
    public class QuotationViewModel : ViewModelBase
    {
        private readonly HttpClient _httpClient;

        private readonly HubConnection _hubConnection;
        private readonly NotificationService _notificationService;
        private AntDesign.DrawerService _drawerService;
        #region Propertise
        //int _pageIndex = 1;

        public DialogService _dialogService { get; set; }
        private int _pageIndex=1;

        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value;
            OnPropertyChanged(nameof(PageIndex)); 
            }
        }
        private int _pageSize;

        public int PageSize
        {
            get { return _pageSize; }
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
        #region Commands
        //public EventCallback AcceptCommand => new EventCallback(null, (Action<object>)(async (object msg) =>
        //{
        //    await CanExecuteAccept(msg);
        //}));
        //public EventCallback RejectCommand => new EventCallback(null, (Action<object>)(async (object msg) =>
        //{
        //    await CanExecuteReject(msg);
        //}));

       
        #endregion
        public IList<Domain.Shared.Deal.Requests.PropositionRequest> Quotations { get; set; } = new List<Domain.Shared.Deal.Requests.PropositionRequest>();

        public QuotationViewModel(HttpClient httpClient, NotificationService notificationService, AntDesign.DrawerService drawerService, DialogService dialogService)
        {
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
          
            this._notificationService = notificationService;
          _drawerService = drawerService;
            _dialogService = dialogService;
          
        }
        public async Task<int> GetTransactionsCountAsync()
        {
          

                return await _httpClient.GetFromJsonAsync<int>(Config.GetDealPaymentsCount);
           
        }
        public async Task GetQuotationsAsync(int pageindex,int size)
        {
            string url = string.Format(Config.GetQuotationByUserId, pageindex, size);
            var ListPaymentsResponse = await _httpClient.GetFromJsonAsync<Result<IEnumerable<Domain.Shared.Deal.Requests.PropositionRequest>>>(url);
           
            if (ListPaymentsResponse.Succeeded)
            {
               
                if (ListPaymentsResponse.Data.Any())
                {
                    Quotations = new List<Domain.Shared.Deal.Requests.PropositionRequest>(ListPaymentsResponse.Data);
                    Total = ListPaymentsResponse.Data.Count();
                    //foreach (var quotations in ListPaymentsResponse.Data)
                    //{

                    //    Quotations.Add(quotations);
                    //};
                }
            }
        }

       
        public override async Task OnInitializedAsync()
        {

            Loading = true;
            //Total = await GetTransactionsCountAsync();
            PageSize = 10;
            await GetQuotationsAsync(0, PageSize);
            StateHasChanged();
            Loading = false;

            await base.OnInitializedAsync();
        }
        public async Task PageIndexChanged(LoadDataArgs args)
        {
            Loading = true;
            await GetQuotationsAsync((int)(args.Skip != null ? args.Skip : 0), args.Top.Value);

            Loading = false;
            StateHasChanged();
        }

        #region CommandMethods
        public async Task CanExecuteReject(PropositionRequest obj)
        {
            obj.Quotation.IsRejected = true;
            obj.Quotation.IsAccepted = false;
            var result = await _httpClient.PostAsJsonAsync(Config.QuotationAccpetReject, obj.Quotation);
            if (result.IsSuccessStatusCode)
            {
                await GetQuotationsAsync(PageIndex, PageSize);
            }
        }

        public async Task CanExecuteAccept(PropositionRequest obj)
        {
            obj.Quotation.IsAccepted = true;
            await _dialogService.OpenAsync<AcceptOrRejectProposition>(
              "",
              new Dictionary<string, object>
              {
                {"Id", obj.Id},
                {"IsOffer", false},
                {"RefreshComponent", RefreshComponent},
                {"TotalAmount", obj.TotalAmount},
                {"Warranty", obj.Quotation?.WarrantyPost?.Description ?? string.Empty},
                {"TermAndCondition", obj.Quotation?.TermConditionPost?.Description},
                {"OwnerId",obj?.OwnerId },
                {"ProposerId",NPCompletApp.ShellViewModel.UserState.User.GetUserId()},
              });
          var result=  await _httpClient.PostAsJsonAsync(Config.QuotationAccpetReject,obj.Quotation);
            if (result.IsSuccessStatusCode)
            {
                var generateInvoice = await _httpClient.PostAsJsonAsync(Config.GenerateInvoice, obj.Quotation);
                if (generateInvoice.IsSuccessStatusCode)
                {
                    var firstName = NPCompletApp.ShellViewModel.UserState.User.GetFirstName();
                    var phoneNumber = NPCompletApp.ShellViewModel.UserState.User.GetPhoneNumber();
                    var email = NPCompletApp.ShellViewModel.UserState.User.GetEmail();
                    await GetQuotationsAsync(PageIndex, PageSize);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<html>");
                    sb.Append("<body>");
                    sb.Append("<p Align='center'> <font size='12'>INVOICE RECEIPT</p></font> ");
                    sb.Append("<div style='display: flex;'>");
                    sb.Append("<div style='width: 50 %; '>");
                    sb.Append("<h5>Name:</h6>" + firstName);
                    sb.Append("<h5>Invoice Date:</h6>" + phoneNumber);
                    sb.Append("</div>");
                    sb.Append("<div style='width: 50 %; '>");
                    sb.Append("<h5>QuotationID</h6>" + obj.QuotationId);
                    sb.Append("<h5>QuotationBY:</h6>" + obj.Quotation.ProposerId);
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("<div Align='center'>");
                    sb.Append("<br>");
                    sb.Append("<table>");
                    sb.Append("<tr>");
                    sb.Append("<th>ID</th>");
                    sb.Append("<th>TotalAmount</th>");
                    sb.Append("<th>PostTitle</th>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append("<td>" + obj.QuotationId + "</td>");
                    sb.Append("<td>" + obj.TotalAmount + "</td>");
                    sb.Append("<td>" + obj.Quotation.Post.Title + "</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                    sb.Append("</body>");
                    sb.Append("</html>");
                    byte[] bytes;
                    StringReader sr = new StringReader(sb.ToString());
                    Rectangle envelope = new Rectangle(432, 252);
                    Document pdfDoc = new Document(envelope, 10f, 10f, 10f, 0f);
                    HtmlWorker htmlparser = new HtmlWorker(pdfDoc);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        pdfDoc.Close();
                        bytes = memoryStream.ToArray();
                        memoryStream.Close();
                    }

                    var a = await _httpClient.GetFromJsonAsync<bool>(string.Format(Config.SendInvoicePdf,email, bytes));


                }

            }
        }
        public EventCallback RefreshComponent => new EventCallback(null, (Action)(async () =>
        {

            await GetQuotationsAsync(0, PageSize);

            StateHasChanged();
        }));
        #endregion
    }
}
