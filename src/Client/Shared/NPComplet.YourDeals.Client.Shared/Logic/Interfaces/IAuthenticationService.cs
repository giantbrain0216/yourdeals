// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthenticationService.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Client.Shared.Logic.Interfaces
{
    #region

    using System.Threading.Tasks;

    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects;
    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.ExternalLogins;
    using NPComplet.YourDeals.Domain.Shared.Wrapper;

    #endregion

    /// <summary>
    /// The AuthenticationService interface.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// The login.
        /// </summary>
        /// <param name="userForAuthentication">
        /// The user for authentication.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<Result<AuthResponseDto>> Login(LoginViewModel userForAuthentication);
        Task<Result<ValidateAccessTokenDto>> ValidateAccessToken(ValidateAccessTokenDto validateFacebookAccessTokenDto);

        Task<Result<AuthResponseDto>> RegisterWithExternalProvider(ValidateAccessTokenDto validateFacebookAccessTokenDto);

        Task<Result<AuthResponseDto>> LoginWithExternalProvider(ExternalLoginViewModel userForAuthenticationnExternal);

        Task<Result<string>> GetFacebookAppId();

        Task<Result<string>> GetGoogleAppId();





        Task<RegistrationResponseDto> Register(RegisterViewModel userForAuthentication);

        Task<bool> ConfirmEmail(ConfirmationBody confirmationBody);

        Task ResendVerification(string id, string RedirectUrl);
        Task<Result<AuthResponseDto>> SendVerificationEmailByEmail(ForgotPasswordViewModel UserForForgotPassword);

        Task<Result<AuthResponseDto>> ForgotPassword(ForgotPasswordViewModel UserForForgotPassword);
        Task<Result<AuthResponseDto>> ResetPassword(ForgotPasswordViewModel UserForForgotPassword, string userId);
        /// <summary>
        /// The logout.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task Logout();
    }
}