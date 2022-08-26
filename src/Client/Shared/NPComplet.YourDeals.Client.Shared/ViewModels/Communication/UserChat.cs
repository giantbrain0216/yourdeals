// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserChat.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.MobileBlazorBindings.Authentication;
using MvvmBlazor.ViewModel;
using NPComplet.YourDeals.Client.Shared.Extensions;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.Logging;
using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Client.Shared.ViewModels.Base;
using NPComplet.YourDeals.Domain.Shared.Communication;
using NPComplet.YourDeals.Domain.Shared.Constant.Application;
using NPComplet.YourDeals.Domain.Shared.Shared;
using Prism.Mvvm;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Communication
{
    #region

    #endregion

    /// <summary>
    ///     The user chat.
    /// </summary>
    public class UserChat : ViewModelBase
    {
        private readonly HttpClient _httpClient;
        public IHttpClientService HttpClientService { get; set; }
        private string _cid;
        private readonly ILogger<UserChat> _logger;
        private Guid _roomId;
        private readonly JsonSerializerOptions _options;


        /// <summary>
        /// Initializes a new instance of the <see cref="UserChat"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        /// <param name="httpInterceptorService"></param>
        /// <param name="tokenInterceptor"></param>
        public UserChat(IHttpClientService httpClientService, ILogger<UserChat> logger)
        {
            this._httpClient = httpClientService.ApplicationHttpClient;
            this._options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            this.CurrentUserId = NPCompletApp.ShellViewModel.UserState.User.GetUserId();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        ///     Gets or sets the access token provider.
        /// </summary>
        public IAccessTokenProvider AccessTokenProvider { get; set; }

        /// <summary>
        ///     Gets or sets the cid.
        /// </summary>
        //public string Cid
        //{
        //    get => this._cid;
        //    set => this.SetProperty(ref this._cid, value);
        //}

        [Parameter]
        public string cid
        {
            get { return _cid; }
            set { _cid = value; 
            OnPropertyChanged(nameof(cid));
            }
        }

        //public Guid RoomId
        //{
        //    get => _roomId;
        //    set => SetProperty(ref _roomId, value);
        //}
        private Guid roomId;
        [Parameter]
        public Guid RoomId
        {
            get { return roomId; }
            set { roomId = value;
            OnPropertyChanged(nameof (RoomId));
            }
        }

        /// <summary>
        ///     Gets or sets the current message.
        /// </summary>
        public string CurrentMessage { get; set; }

        /// <summary>
        ///     Gets or sets the current user id.
        /// </summary>
        public string CurrentUserId { get; set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        public Message Message { get; set; }

        /// <summary>
        ///     Gets or sets the user messages.
        /// </summary>
        public List<Message> UserMessages { get; set; } = new List<Message>();

        /// <summary>
        /// The get messages.
        /// </summary>
        /// <param name="cid">
        /// The cid.
        /// </param>
        /// <param name="roomId">The deal or proposition messages post identifier.</param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task GetMessages(string cid, Guid roomId)
        {
            NPCompletApp.ShellViewModel.isBusy = true;
            UserMessages = new List<Message>();
            try
            {
                var messages = await _httpClient.GetFromJsonAsync<IEnumerable<Message>>($"{Config.GetUserConvertion}/{cid}/{roomId}");

                if (messages != null && messages.Any())
                {
                    foreach (var message in messages)
                    {
                        UserMessages.Add(message);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            NPCompletApp.ShellViewModel.isBusy = false;
        }


        /// <summary>
        /// The submit async.
        /// </summary>
        /// <param name="cid">
        /// The cid.
        /// </param>
        /// <param name="roomId">The deal or proposition messages post identifier.</param>
        /// <param name="isDeal">True if the chat is about deal.</param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<bool> SubmitAsync(string cid, Guid roomId, bool isDeal)
        {
            NPCompletApp.ShellViewModel.isBusy = true;
            if (string.IsNullOrEmpty(CurrentMessage) || string.IsNullOrEmpty(cid))
            {
                return false;
            }

            var message = new Message
            {
                OwnerId = Guid.Parse(CurrentUserId),
                ToUserId = Guid.Parse(cid),
                MessageCore = new Post
                {
                    Description = CurrentMessage
                },
                SendingTimeUtc = DateTime.UtcNow,
                IsOpen = true
            };

            if (isDeal)
                message.DealMessagesPostId = roomId;
            else
                message.PropositionMessagesPostId = roomId;

            var result = await _httpClient.PostAsJsonAsync(Config.SaveMessage, message);
            var content = await result.Content.ReadAsStringAsync();
            var messageResult = JsonSerializer.Deserialize<Message>(content, _options);


            NPCompletApp.ShellViewModel.isBusy = false;
            if (result.IsSuccessStatusCode)
            {
                Message = messageResult;
                return true;
            }

            return false;
        }

      

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        [Parameter]
        public string Style { get; set; }


        [Parameter]
        public bool IsDeal { get; set; } = false;

        [Parameter]
        public string CurrentUserBase64 { get; set; }

        private HubConnection HubConnection { get; set; }

        /// <summary>
        /// The on initialized async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override async Task OnInitializedAsync()
        {
            PropertyChanged += (e, s) => StateHasChanged();

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

            HubConnection.On<Message, string>(ApplicationConstants.SignalR.ReceiveMessage, async (chatHistory, userName) =>
            {
                if (cid == chatHistory.ToUserId.ToString() && CurrentUserId == chatHistory.OwnerId.ToString() || cid == chatHistory.OwnerId.ToString() && CurrentUserId == chatHistory.ToUserId.ToString())
                {
                    if (cid == chatHistory.ToUserId.ToString() && CurrentUserId == chatHistory.OwnerId.ToString())
                    {
                        // _messages.Add(new ChatHistoryResponse { Message = chatHistory.Message, FromUserFullName = userName, CreatedDate = chatHistory.CreatedDate, FromUserImageURL = CurrentUserImageURL });
                        UserMessages.Add(chatHistory);

                        // await HubConnection.SendAsync(ApplicationConstants.SignalR.SendChatNotification, string.Format(_localizer["New Message From {0}"], userName), CId, CurrentUserId);
                    }
                    else if (cid == chatHistory.OwnerId.ToString() && CurrentUserId == chatHistory.ToUserId.ToString())
                    {
                        // _messages.Add(new ChatHistoryResponse { Message = chatHistory.Message, FromUserFullName = userName, CreatedDate = chatHistory.CreatedDate, FromUserImageURL = CImageURL });
                        UserMessages.Add(chatHistory);
                    }

                    StateHasChanged();
                }
            });
        }

        /// <summary>
        /// The get access token value async.
        /// </summary>
        /// <returns>
        /// The <see cref="ValueTask"/>.
        /// </returns>
       public async ValueTask<string> GetAccessTokenValueAsync()
        {
            var result = NPCompletApp.Token;
            return result;
        }

        /// <summary>
        /// The submit.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
       public async Task Submit()
        {
            if (await SubmitAsync(cid, RoomId, IsDeal))
            {
                var userName = NPCompletApp.ShellViewModel.UserState.User.GetEmail();
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendMessage, Message, userName);
                CurrentMessage = string.Empty;
            }

            StateHasChanged();
        }

        /// <summary>
        /// Get messages.
        /// </summary>
       public async Task GetMessages()
        {
            if (cid != null)
            {
                await GetMessages(cid, RoomId);
                StateHasChanged();
            }
        }
    }
}