// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGetUserVM.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Users;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Identity.Interfaces
{
    #region

    #endregion

    /// <summary>
    ///     The GetUserVM interface.
    /// </summary>
    public interface IGetUserVM
    {
        /// <summary>
        ///     The get all users.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task<List<User>> GetAllUsers();

        /// <summary>
        /// The get user by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<User> GetUserById(Guid? id);
    }
}