// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TwoFactorAuthenticationViewModel.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects.ManageViewModels
{
    /// <summary>
    ///     TwoFactorAuthenticationViewModel
    /// </summary>
    public class TwoFactorAuthenticationViewModel
    {
        /// <summary>
        ///     HasAuthenticator
        /// </summary>
        public bool HasAuthenticator { get; set; }

        /// <summary>
        ///     Is2FaEnabled
        /// </summary>
        public bool Is2FaEnabled { get; set; }

        /// <summary>
        ///     RecoveryCodesLeft
        /// </summary>
        public int RecoveryCodesLeft { get; set; }
    }
}