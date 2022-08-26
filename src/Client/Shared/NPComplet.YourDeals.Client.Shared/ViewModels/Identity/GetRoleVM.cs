// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetRoleVM.cs" company="NPComplet">
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
    ///     The get role vm.
    /// </summary>
    public class GetRoleVM : BindableBase, IGetRoleVM
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRoleVM"/> class.
        ///     ctor
        /// </summary>
        /// <param name="httpClient">
        /// </param>
        public GetRoleVM(HttpClient httpClient)
        {
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        /// <summary>
        ///     Get All Roles
        /// </summary>
        /// <returns>Roles.</returns>
        public async Task<List<Role>> GetAllRoles()
        {
            try
            {
                return await this._httpClient.GetFromJsonAsync<List<Role>>(Config.GetAllRoles);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}