using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MvvmBlazor.ViewModel;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.Logging;
using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Client.Shared.ViewModels.Base;
using NPComplet.YourDeals.Client.Shared.ViewModels.Deals.Interfaces;
using NPComplet.YourDeals.Client.Shared.WebUI.Pages.Deals.DealPage;
using NPComplet.YourDeals.Client.Shared.WebUI.Shared;
using NPComplet.YourDeals.Domain.Shared.Accounting;
using NPComplet.YourDeals.Domain.Shared.Communication;
using NPComplet.YourDeals.Domain.Shared.Deal;
using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
using NPComplet.YourDeals.Domain.Shared.Deal.Requests;
using NPComplet.YourDeals.Domain.Shared.Enumerable;
using NPComplet.YourDeals.Domain.Shared.Finance;
using NPComplet.YourDeals.Domain.Shared.Pricing;
using NPComplet.YourDeals.Domain.Shared.Shared;
using NPComplet.YourDeals.Translations.Resources;

using Radzen;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Deals
{
    /// <summary>
    /// The proposition view model.
    /// </summary>
    public class PropositionViewModel : ViewModelBase
    {
        private readonly ILogger<PropositionViewModel> _logger;
        private readonly HttpClient _httpClient;
        public DialogService _dialogService;
        private readonly NotificationService _notificationService;
        private readonly IStringLocalizer<Translation> _localizer;


        public PropositionOffer PropositionOffer { get; set; }
        public PropositionRequest PropositionRequest { get; set; }
        public ImageUpload ImageUpload { get; set; }

        [Parameter]
        public Guid? OwnerId { get; set; }
        [Parameter]
        public string ProposerId { get; set; }
        public PropositionViewModel(ILogger<PropositionViewModel> logger,
            IHttpClientService httpClientService,
            DialogService dialogService,
            NotificationService notificationService,
            IStringLocalizer<Translation> localizer)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClientService.ApplicationHttpClient ?? throw new ArgumentNullException(nameof(httpClientService.ApplicationHttpClient));
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _localizer = localizer ?? throw new ArgumentNullException(nameof(localizer));
      
          
 
        }

        /// <inheritdoc />
        public async Task CreatePropositionOfferAsync(PropositionOffer propositionOffer, Guid id, Guid dealMessagePostId, EventCallback refreshComponent)
        {
            try
            {
                Guid.TryParse(ProposerId, out Guid guid);
                propositionOffer.OfferId = id;
                propositionOffer.Quotation = new Quotation();
                propositionOffer.Quotation.OwnerId = OwnerId;
                propositionOffer.Quotation.ProposerId = guid;

                if ((await _httpClient.PostAsJsonAsync(Config.CreatePropositionOffer, propositionOffer)).IsSuccessStatusCode)
                {
                    await NotifyAndRefreshComponentAsync(refreshComponent, _localizer["PropositionCreationMessage"]);
                    await CloseDealMessages(dealMessagePostId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task CreatePropositionRequestAsync(PropositionRequest propositionRequest, Guid id, Guid dealMessagePostId, EventCallback refreshComponent)
        {
            try
            {
                Guid.TryParse(ProposerId, out Guid guid);
                propositionRequest.RequestId = id;
                propositionRequest.Document = new DealDocument(ImageUpload.ImgUrl);
                propositionRequest.Quotation.PostId = dealMessagePostId;
                propositionRequest.Quotation.OwnerId = OwnerId;
                propositionRequest.Quotation.ProposerId = guid;

                if ((await _httpClient.PostAsJsonAsync(Config.CreatePropositionRequest, propositionRequest)).IsSuccessStatusCode)
                {
                    await NotifyAndRefreshComponentAsync(refreshComponent, _localizer["PropositionCreationMessage"]);
                    await CloseDealMessages(dealMessagePostId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task AcceptPropositionAsync(bool isOffer, Guid propositionId, EventCallback refreshComponent)
        {
            try
            {
                if (isOffer)
                {
                    await _httpClient.PutAsJsonAsync<PropositionOffer>(string.Format(Config.SelectPropositionOffer, propositionId), null);
                }
                else
                {
                    await _httpClient.PutAsJsonAsync<PropositionRequest>(string.Format(Config.SelectPropositionRequest, propositionId), null);
                }

                await NotifyAndRefreshComponentAsync(refreshComponent, _localizer["PropositionAcceptanceMessage"]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private async Task CloseDealMessages(Guid dealMessagesPostId)
        {
            try
            {
                IList<Message> messagesToClose = await _httpClient.GetFromJsonAsync<IList<Message>>(string.Format(Config.RoomMessages, dealMessagesPostId));
                await _httpClient.PutAsJsonAsync(Config.CloseMessages, messagesToClose);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

     
     

        private async Task NotifyAndRefreshComponentAsync(EventCallback refreshComponent, string notificationDetail)
        {
            _notificationService.Notify(
                new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Duration = 1000,
                    Summary = _localizer["Success"],
                    Detail = notificationDetail
                });

            await refreshComponent.InvokeAsync(null);
            _dialogService.Close();
        }
        #region Parameters
      
        //public Guid DealMessagePostId { get; set; }
        private Guid _dealMessagePostId;
        [Parameter]
        public Guid DealMessagePostId
        {
            get { return _dealMessagePostId; }
            set { _dealMessagePostId = value;
            OnPropertyChanged(nameof(DealMessagePostId)); 
            }
        }

        //public Guid Id { get; set; }
        private Guid _id;
        [Parameter]
        public Guid Id
        {
            get { return _id; }
            set { _id = value; 
            OnPropertyChanged(nameof(Id));
            }
        }

        
        //public bool IsOffer { get; set; }
        private bool _isOffer;
        [Parameter]
        public bool IsOffer
        {
            get { return _isOffer; }
            set { _isOffer = value;
            OnPropertyChanged(nameof(IsOffer));
            }
        }

        
        //public decimal TotalAmount { get; set; }
        private decimal _totalAmount;
        [Parameter]
        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; 
            OnPropertyChanged(nameof(TotalAmount));
            }
        }

        
        //public string Title { get; set; }
        private string _title;
        [Parameter]
        public string Title
        {
            get { return _title; }
            set { _title = value;
            OnPropertyChanged(nameof(Title));
            }
        }

        [Parameter]
        public EventCallback RefreshComponent { get; set; }
        


        
        //public string Warranty { get; set; }
        private string _warranty;
        [Parameter]
        public string Warranty
        {
            get { return _warranty; }
            set { _warranty = value;
            OnPropertyChanged(nameof(Warranty));
            }
        }

        
        //public string TermAndCondition { get; set; }
        private string _termAndCondition;
        [Parameter]
        public string TermAndCondition
        {
            get { return _termAndCondition; }
            set { _termAndCondition = value;
            OnPropertyChanged(nameof(TermAndCondition));
            }
        }


        
        private bool _isTermAccepted;

        public bool IsTermAccepted
        {
            get { return _isTermAccepted; }
            set { _isTermAccepted = value; }
        }
        
        //public List<PaymentManor> PaymentManors { get; set; }
        private List<PaymentManor> _paymentManors;
        [Parameter]
        public List<PaymentManor> PaymentManors
        {
            get { return _paymentManors; }
            set { _paymentManors = value; }
        }

        #endregion
        public async Task AcceptProposition()
        {
            await AcceptPropositionAsync(IsOffer, Id, RefreshComponent);
        }

        public async Task OpenTermAndWarranty()
        {
            await _dialogService.OpenAsync<WarrantyAndTerm>(
                "",
                new Dictionary<string, object>
                {
                { "WarrantyDescription", Warranty },
                { "TermDescription", TermAndCondition },
                },
                new Radzen.DialogOptions { Width = "700px", Height = "500px", Resizable = true, Draggable = true });
        }

        public void AddOfferAmount()
        {
            PropositionOffer.Invoice.Amounts.Add(new Amount());
        }

       

        public void AddRequestAmount()
        {
            PropositionRequest.Quotation.Amounts.Add(new Amount());
        }

        public override void OnInitialized()
        {
            PropositionOffer = new PropositionOffer
            {
                Invoice = new Invoice
                {
                    Amounts = new List<Amount>
        {
                    new Amount()
                }
                },
                PropositionMessagesPost = new PropositionMessagesPost(),
                PropositionsFinanceOperation = new PropositionsFinanceOperation()
            };

            PropositionRequest = new PropositionRequest
            {
                Quotation = new Quotation
                {
                    Amounts = new List<Amount>
        {
                    new Amount()
                },
                    WarrantyPost = new Post(),
                    TermConditionPost = new Post()
                },
                PropositionMessagesPost = new PropositionMessagesPost(),
                PropositionsFinanceOperation = new PropositionsFinanceOperation()
            };
        }

        public void UpdateOfferAmount(PaymentManor? manor)
        {
            if (manor == PaymentManor.PayAfter || manor == PaymentManor.PayBefore)
                PropositionOffer.Invoice.Amounts = PropositionOffer.Invoice.Amounts.Take(1).ToList();
        }

        public void UpdateRequestAmount(PaymentManor? manor)
        {
            if (manor == PaymentManor.PayAfter || manor == PaymentManor.PayBefore)
                PropositionRequest.Quotation.Amounts = PropositionRequest.Quotation.Amounts.Take(1).ToList();
        }

        public void DeleteOfferAmount(int i)
        {
            var amounts = PropositionOffer.Invoice.Amounts.ToList();
            amounts.RemoveAt(i);
            PropositionOffer.Invoice.Amounts = amounts;
        }

        public void DeleteRequestAmount(int i)
        {
            var amounts = PropositionRequest.Quotation.Amounts.ToList();
            amounts.RemoveAt(i);
            PropositionRequest.Quotation.Amounts = amounts;
        }
    }
}
