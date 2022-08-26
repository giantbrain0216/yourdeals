// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGetRoleVM.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Users;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Identity.Interfaces
{
    #region

    #endregion

    /// <summary>
    ///     The GetRoleVM interface.
    /// </summary>
    public interface IGetRoleVM
    {
        /// <summary>
        ///     The get all roles.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task<List<Role>> GetAllRoles();
    }
}