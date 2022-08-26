// --------------------------------------------------------------------------------------------------------------------
// <copyright file="cs" company="NPComplet">s
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.Logging;
using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Client.Shared.ViewModels.Base;
using NPComplet.YourDeals.Client.Shared.ViewModels.Communication.Interfaces;
using NPComplet.YourDeals.Domain.Shared.Communication;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Communication
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.MobileBlazorBindings;
    using MvvmBlazor.ViewModel;
    using NPComplet.YourDeals.Client.Shared.Extensions;
    using NPComplet.YourDeals.Domain.Shared.Constant.Application;
    using NPComplet.YourDeals.Domain.Shared.Users;
    #region

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The chat history view model.
    /// </summary>
    public class ChatHistoryViewModel : ViewModelBase
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
        public ChatHistoryViewModel(IHttpClientService httpClientService)
        {
            this._httpClient = httpClientService.ApplicationHttpClient;
            //_shellNavigationManager = shellNavigationManager;

        }

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
        #region Command

        #endregion
        /// <summary>
        /// Gets or sets the user messages.
        /// </summary>
        public List<Message> UserMessages { get; set; }

        /// <summary>
        /// Gets or sets the cid.
        /// </summary>
        public string Cid { get; set; }

        public Guid RoomId { get; set; }

        public bool IsDeal { get; set; }

        public string SelectedToUserBase64 { get; set; }

        
        //public string MessageId { get; set; }

        private string _messageId;
        [Parameter]
        public string MessageId
        {
            get { return _messageId; }
            set { _messageId = value; }
        }



        public EventCallback GetContactMessageCommand => new EventCallback(null, (Action<Message>)((Message msg) =>
        {
            GetContactMessage(msg);
            //StateHasChanged();
        }));
        public EventCallback GetContactMsgsCommand => new EventCallback(null, (Action<Message>)(async (Message msg) =>
        {
            await GetContactMsgs(msg);
            StateHasChanged();
        }));
       

  


        public string cid { get; set; }
        private HubConnection hubConnection;
     

        public async Task GetContactMsgs(Message msg)
        {

            await _shellNavigationManager.NavigateToAsync("/mobilechat/" + msg.ToUserId.ToString());

        }


       


       

        /// <summary>
        /// The on initialized async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public override async Task OnInitializedAsync()
        {
            UserMessages = new List<Message>();
            var messages = await GetAllUserMessages();
            if (messages.Count > 0)
            {
                foreach (var msg in messages)
                {
                    UserMessages.Add(msg);
                }
            }


        }
        public override Task OnParametersSetAsync()
        {
            if (!String.IsNullOrEmpty(MessageId))
            {
                var selectedMessage = UserMessages.Find(x => x.Id.ToString() == MessageId);
                GetContactMessage(selectedMessage);
            }
            return base.OnParametersSetAsync();
        }
        //public override async Task OnParametersSetAsync()
        //{
        //    if (!String.IsNullOrEmpty(MessageId))
        //    {
        //        var selectedMessage = UserMessages.Find(x => x.Id.ToString() == MessageId);
        //        GetContactMessage(selectedMessage);
        //    }

        //}

        /// <summary>
        /// The get contact msgs.
        /// </summary>
        /// <param name="msg">
        /// The msg.
        /// </param>
        public void GetContactMessage(Message msg)
        {
            Cid = !NPCompletApp.ShellViewModel.UserState.User.GetUserId().Contains(msg.ToUserId.ToString()) ? msg.ToUserId.ToString() : msg.OwnerId.ToString();

            RoomId = msg.DealMessagesPostId ?? msg.PropositionMessagesPostId ?? default;

            IsDeal = msg.DealMessagesPostId != null;

            SelectedToUserBase64 = convertImgToBase64(msg.ToUser);

            StateHasChanged();
        }

        public string convertImgToBase64(User user)
        {
            string Base64 =
            "data:image/"
                    + $"{user.Profile.ProfileImage.FileExtension};"
                    + "base64, "
                    + Convert.ToBase64String(user.Profile.ProfileImage.Data);

            return Base64;
        }


    }
}