// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IChatHistoryViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Communication;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Communication.Interfaces
{
    #region

    #endregion

    /// <summary>
    ///     The ChatHistoryViewModel interface.
    /// </summary>
    public interface IChatHistoryViewModel
    {
        /// <summary>
        ///     The get all user messages.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task<IList<Message>> GetAllUserMessages();
    }
}