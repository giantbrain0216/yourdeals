
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.MobileBlazorBindings;
using MvvmBlazor.ViewModel;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Domain.Shared.Communication;
using NPComplet.YourDeals.Domain.Shared.Constant.Application;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Communication
{
    public class MobileChatHistoryViewModel:ViewModelBase
    {

        private readonly HttpClient _httpClient;
        private ShellNavigationManager _shellNavigationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChatHistoryViewModel"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        /// <param name="httpClientService"></param>
        public MobileChatHistoryViewModel(IHttpClientService httpClientService)
        {
            this._httpClient = httpClientService.ApplicationHttpClient;
            //_shellNavigationManager = shellNavigationManager;

        }
        #region Command
        public EventCallback GetContactMsgsCommand => new EventCallback(null, (Action<Message>)(async (Message msg) =>
        {
            await GetContactMsgs(msg);
            StateHasChanged();
        }));
        #endregion
        /// <summary>
        /// The get all user messages.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<IList<Message>> GetAllUserMessages()
        {

            return await this._httpClient.GetFromJsonAsync<List<Message>>(Config.GetUserAllChatHistory);
        }

        public List<Message> UserMessages { get; set; }

        public string cid { get; set; }
        private HubConnection hubConnection;
        public override async Task OnInitializedAsync()
        {
            UserMessages = new List<Message>();
            var msgs = await GetAllUserMessages();
            if (msgs.Count > 0)
            {
                foreach (var msg in msgs)
                {
                    UserMessages.Add(msg);
                }

            }
            hubConnection = new HubConnectionBuilder()
                        .WithUrl(Config.BaseUrl + ApplicationConstants.SignalR.HubUrl, options =>
                        {
                        //options.SkipNegotiation = true;

                        //options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                            options.Headers.Add("Authorization", string.Format("Bearer {0}", NPCompletApp.Token));


                            options.AccessTokenProvider = async () =>
                            {


                                return await Task.FromResult(NPCompletApp.Token);

                            };
                        })
                .WithAutomaticReconnect()
                .Build();
            if (hubConnection.State == HubConnectionState.Disconnected)
            {
                await hubConnection.StartAsync();
            }


        }

     public   async Task GetContactMsgs(Message msg)
        {
            await _shellNavigationManager.NavigateToAsync("/mobilechat/" + msg.ToUserId.ToString());

        }
    }
}
