#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Communication;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces
{
    /// <summary>
    ///     Message service interface.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        ///     Get user chat history.
        /// </summary>
        /// <param name="currentUserId">The authenticated user identifier.</param>
        /// <returns>The selected messages.</returns>
        public Task<IList<Message>> GetUserChatHistory(Guid currentUserId);
    }
}