// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SignalRHub.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Hubs
{
    using System;
    using System.Collections.Generic;
    #region

    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;

    using NPComplet.YourDeals.Domain.Shared.Communication;
    using NPComplet.YourDeals.Domain.Shared.Constant.Application;
    using NPComplet.YourDeals.Domain.Shared.Users;

    #endregion



    /// <summary>
    ///     The signalR hub class.
    /// </summary>
    [Authorize]
    public class SignalRHub : Hub
    {
        protected readonly UserManager<User> _userManager;



        public SignalRHub(UserManager<User> userManager)
        {
            _userManager = userManager;

        }



        /// <summary>
        /// On connect asynchronously.
        /// </summary>
        /// <param name="userId">
        /// The user identifier.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>

        public async Task OnConnectAsync(string userId)
        {
            await this.Clients.All.SendAsync(ApplicationConstants.SignalR.ConnectUser, userId);
        }

        /// <summary>
        /// On disconnect asynchronously.
        /// </summary>
        /// <param name="userId">
        /// The user identifier.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task OnDisconnectAsync(string userId)
        {

            await this.Clients.All.SendAsync(ApplicationConstants.SignalR.DisconnectUser, userId);
        }


        /// <summary>
        /// Chat notification asynchronously.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="receiverUserId">
        /// Th receiver user identifier.
        /// </param>
        /// <param name="senderUserId">
        /// The sender user identifier.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId)
        {
            await this.Clients.User(receiverUserId).SendAsync(
                ApplicationConstants.SignalR.ReceiveChatNotification,
                message,
                receiverUserId,
                senderUserId);
        }

        /// <summary>
        /// On change role permissions.
        /// </summary>
        /// <param name="userId">
        /// The user identifier.
        /// </param>
        /// <param name="roleId">
        /// The role identifier.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        
        public async Task OnChangeRolePermissions(string userId, string roleId)
        {
            await this.Clients.All.SendAsync(ApplicationConstants.SignalR.LogoutUsersByRole, userId, roleId);
        }




       

        /// <summary>
        /// Ping request asynchronously.
        /// </summary>
        /// <param name="userId">
        /// The user who sends the ping request.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task PingRequestAsync(string userId)
        {
            await this.Clients.All.SendAsync(ApplicationConstants.SignalR.PingRequest, userId);
        }

        /// <summary>
        /// Ping response asynchronously.
        /// </summary>
        /// <param name="userId">
        /// The user identifier.
        /// </param>
        /// <param name="requestedUserId">
        /// The user who sends the ping reques and should recive the results.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>

        public async Task PingResponseAsync(string userId, string requestedUserId)
        {
            await this.Clients.User(requestedUserId).SendAsync(ApplicationConstants.SignalR.PingResponse, userId);
        }

        /// <summary>
        /// Regenerate tokens asynchronously.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task RegenerateTokensAsync()
        {
            await this.Clients.All.SendAsync(ApplicationConstants.SignalR.ReceiveRegenerateTokens);
        }

        /// <summary>
        /// Send message asynchronously.
        /// </summary>
        /// <param name="chatHistory">
        /// The chat history message.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
       
        public async Task SendMessageAsync(Message chatHistory, string userName)
        {
            await this.Clients.User(chatHistory.ToUserId.ToString()).SendAsync(
                ApplicationConstants.SignalR.ReceiveMessage,
                chatHistory,
                userName);

            if (chatHistory.ToUserId != chatHistory.OwnerId && chatHistory.OwnerId.HasValue)
                await this.Clients.User(chatHistory.OwnerId.Value.ToString()).SendAsync(
                    ApplicationConstants.SignalR.ReceiveMessage,
                    chatHistory,
                    userName);
        }

        /// <summary>
        /// Update dashboard asynchronously.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task UpdateDashboardAsync()
        {
            await this.Clients.All.SendAsync(ApplicationConstants.SignalR.ReceiveUpdateDashboard);
        }
    }
}