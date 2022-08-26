// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentUser.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels
{
    #region

    #endregion

    /// <summary>
    ///     Current User Information
    /// </summary>
    public class CurrentUser
    {
        /// <summary>
        /// </summary>
        public List<KeyValuePair<string, string>> Claims { get; set; }

        /// <summary>
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// </summary>
        public string UserName { get; set; }
    }
}