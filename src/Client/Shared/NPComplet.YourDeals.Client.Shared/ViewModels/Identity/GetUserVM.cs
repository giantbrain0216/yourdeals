// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetUserVM.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.ViewModels.Identity.Interfaces;
using NPComplet.YourDeals.Domain.Shared.Users;
using Prism.Mvvm;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Identity
{
    #region

    #endregion

    /// <summary>
    ///     The get user vm.
    /// </summary>
    public class GetUserVM : BindableBase, IGetUserVM
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserVM"/> class.
        ///     Initialize new instance of <see cref="GetUserVM"/>
        /// </summary>
        /// <param name="httpClient">
        /// The HTTP client.
        /// </param>
        public GetUserVM(HttpClient httpClient)
        {
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        ///     Get All Users
        /// </summary>
        /// <returns>Users.</returns>
        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                return await this._httpClient.GetFromJsonAsync<List<User>>(Config.GetAllUsers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Get user by identifier.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The user.
        /// </returns>
        public async Task<User> GetUserById(Guid? id)
        {
            try
            {
                return await this._httpClient.GetFromJsonAsync<User>(Config.GetUserById);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}