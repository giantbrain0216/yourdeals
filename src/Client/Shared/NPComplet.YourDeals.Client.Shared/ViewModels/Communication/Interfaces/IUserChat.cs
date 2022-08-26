// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUserChat.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using NPComplet.YourDeals.Domain.Shared.Communication;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Communication.Interfaces
{
    #region

    #endregion

    /// <summary>
    ///     The UserChat interface.
    /// </summary>
    public interface IUserChat
    {
        /// <summary>
        ///     Gets the cid.
        /// </summary>
        string cid { get; set; }

        /// <summary>
        ///     Gets the current message.
        /// </summary>
        string CurrentMessage { get; set; }

        /// <summary>
        ///     Gets the current user id.
        /// </summary>
        string CurrentUserId { get; set; }

        /// <summary>
        ///     Gets the hub connection.
        /// </summary>
        HubConnection HubConnection { get; set; }

        /// <summary>
        ///     Gets the user messages.
        /// </summary>
        List<Message> UserMessages { get; set; }

        /// <summary>
        /// The get messages.
        /// </summary>
        /// <param name="cid">
        /// The cid.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task GetMessages(string cid);

        /// <summary>
        /// The submit async.
        /// </summary>
        /// <param name="cid">
        /// The cid.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task SubmitAsync(string cid);
    }
}