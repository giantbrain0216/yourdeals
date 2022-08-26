// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICreateUserVM.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Users;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Identity.Interfaces
{
    #region

    #endregion

    /// <summary>
    ///     The CreateUserVM interface.
    /// </summary>
    public interface ICreateUserVM
    {
        /// <summary>
        /// The create user.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<User> CreateUser(User user);
    }
}