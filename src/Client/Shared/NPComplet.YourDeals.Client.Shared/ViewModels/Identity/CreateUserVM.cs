// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateUserVM.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
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
    ///     The create user vm.
    /// </summary>
    public class CreateUserVM : BindableBase, ICreateUserVM
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserVM"/> class.
        ///     Initialize new instance of <see cref="CreateUserVM"/>
        /// </summary>
        /// <param name="httpClient">
        /// The HTTP client.
        /// </param>
        public CreateUserVM(HttpClient httpClient)
        {
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        /// Creates user.
        /// </summary>
        /// <param name="user">
        /// The user to create
        /// </param>
        /// <returns>
        /// Created user.
        /// </returns>
        public async Task<User> CreateUser(User user)
        {
            try
            {
                await this._httpClient.PutAsJsonAsync(Config.CreateUser, user);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}