// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveLoginViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects.ManageViewModels
{
    /// <summary>
    ///     RemoveLoginViewModel
    /// </summary>
    public class RemoveLoginViewModel
    {
        /// <summary>
        ///     LoginProvider
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        ///     ProviderKey
        /// </summary>
        public string ProviderKey { get; set; }
    }
}