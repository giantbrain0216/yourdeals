// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationService.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Client.Shared.Logic
{
    #region

    using AuthProviders;
    using Helpers;
    using Interfaces;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.MobileBlazorBindings.ProtectedStorage;
    using NPComplet.YourDeals.Client.Shared.Logging;
    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects;
    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.ExternalLogins;
    using NPComplet.YourDeals.Domain.Shared.Wrapper;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// The authentication service.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authStateProvider;

        private readonly HttpClient _client;

        private readonly IProtectedStorage _localStorage;

        private readonly JsonSerializerOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="client"> The client. </param>
        /// <param name="authStateProvider"> The auth state provider. </param>
        /// <param name="localStorage"> The local storage. </param>
        public AuthenticationService(
            HttpClient client,
            AuthenticationStateProvider authStateProvider,
            IProtectedStorage localStorage)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;

        }

        /// <summary>
        /// The login.
        /// </summary>
        /// <param name="userForAuthentication"> The user for authentication./// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<Result<AuthResponseDto>> Login(LoginViewModel userForAuthentication)
        {
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authResult = await _client.PostAsync(Config.Login, bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Result<AuthResponseDto>>(authContent, _options);

            if (result == null || !result.Succeeded)
            {
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                return result;
            }

            await _localStorage.SetAsync("authToken", result.Data.Token);
            await _localStorage.SetAsync("refreshToken", result.Data.RefreshToken);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthentication.Email);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Data.Token);
            NPCompletApp.ShellViewModel.isLoggedIn = true;

            return result;
        }



        public async Task<Result<AuthResponseDto>> LoginWithExternalProvider(ExternalLoginViewModel userForAuthenticationnExternal)
        {
            var content = JsonSerializer.Serialize(userForAuthenticationnExternal);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authResult = await _client.PostAsync(Config.ExternalLogin, bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Result<AuthResponseDto>>(authContent, _options);

            if (result == null || !result.Succeeded)
            {
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                return result;
            }

            await _localStorage.SetAsync("authToken", result.Data.Token);
            await _localStorage.SetAsync("refreshToken", result.Data.RefreshToken);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthenticationnExternal.Email);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Data.Token);
            NPCompletApp.ShellViewModel.isLoggedIn = true;

            return result;
        }

        public async Task<Result<ValidateAccessTokenDto>> ValidateAccessToken(ValidateAccessTokenDto validateAccessTokenDto)
        {
            var content = JsonSerializer.Serialize(validateAccessTokenDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");


            var validationResult = await _client.PostAsync(Config.ValidateAccessToken, bodyContent);
            var validationContent = await validationResult.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Result<ValidateAccessTokenDto>>(validationContent, _options);


            return result;
        }


        public async Task<RegistrationResponseDto> Register(RegisterViewModel userForAuthentication)
        {
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authResult = await _client.PostAsync(Config.Register, bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<RegistrationResponseDto>(authContent, _options);
        }

        public async Task<bool> ConfirmEmail(ConfirmationBody confirmationBody)
        {
            var content = JsonSerializer.Serialize(confirmationBody);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var authResult = await _client.PostAsync(Config.ConfirmEmail, bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<bool>(authContent, _options);
        }

        public async Task ResendVerification(string id, string RedirectUrl)
        {

            await _client.GetAsync($"{Config.SendVerificationEmail}/{id}/{RedirectUrl}");
        }

        /// <summary>
        /// The logout.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task Logout()
        {
            await _localStorage.DeleteAsync("authToken");
            await _localStorage.DeleteAsync("refreshToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();

            _client.DefaultRequestHeaders.Authorization = null;
            NPCompletApp.ShellViewModel.isLoggedIn = false;
        }




        public async Task<Result<AuthResponseDto>> SendVerificationEmailByEmail(ForgotPasswordViewModel UserForForgotPassword)
        {
            Result<AuthResponseDto> result = new Result<AuthResponseDto>();
            var content = JsonSerializer.Serialize(UserForForgotPassword);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var forgotPasswordResult = await _client.PostAsync(Config.SendVerificationEmail, bodyContent);
            if (!forgotPasswordResult.IsSuccessStatusCode)
            {
                result = new Result<AuthResponseDto> { Succeeded = false, Messages = new List<string> { "Failed to send verification mail due to error." } };
                return result;
            }
            var forgotPasswordResultContent = await forgotPasswordResult.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<Result<AuthResponseDto>>(forgotPasswordResultContent, _options);

            if (result == null || !result.Succeeded)
            {
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                return result;
            }
            return result;
        }

        public async Task<Result<AuthResponseDto>> ForgotPassword(ForgotPasswordViewModel UserForForgotPassword)
        {
            Result<AuthResponseDto> result = new Result<AuthResponseDto>();
            var content = JsonSerializer.Serialize(UserForForgotPassword);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var forgotPasswordResult = await _client.PostAsync(Config.ForgotPassword, bodyContent);
            if (!forgotPasswordResult.IsSuccessStatusCode)
            {
                result = new Result<AuthResponseDto> { Succeeded = false, Messages = new List<string> { "Failed to send Forget Password mail due to error." } };
                return result;
            }
            var forgotPasswordResultContent = await forgotPasswordResult.Content.ReadAsStringAsync();
            result = JsonSerializer.Deserialize<Result<AuthResponseDto>>(forgotPasswordResultContent, _options);

            if (result == null || !result.Succeeded)
            {
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                return result;
            }
            return result;
        }

        public async Task<Result<AuthResponseDto>> ResetPassword(ForgotPasswordViewModel UserForForgotPassword, string userId)
        {
            var resetPasswordResult = await _client.GetAsync($"{Config.ResetPassword}?userId={userId}&password={UserForForgotPassword.NewPassword}&token={UserForForgotPassword.CopiedToken}");
            if (!resetPasswordResult.IsSuccessStatusCode)
                throw new ApplicationException("Something went wrong while processing password change");
            var resetPasswordContent = await resetPasswordResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Result<AuthResponseDto>>(resetPasswordContent, _options);

            if (result == null || !result.Succeeded)
            {
                NPCompletApp.ShellViewModel.isLoggedIn = false;
                return result;
            }
            NPCompletApp.ShellViewModel.isLoggedIn = true;

            return result;
        }

        public Task<Result<AuthResponseDto>> RegisterWithExternalProvider(ValidateAccessTokenDto validateFacebookAccessTokenDto)
        {
            throw new ApplicationException("Not implemented Method Exception");
        }



        Task<Result<string>> IAuthenticationService.GetFacebookAppId()
        {
            throw new ApplicationException("Not implemented Method Exception");
        }

        public Task<Result<string>> GetGoogleAppId()
        {
            throw new ApplicationException("Not implemented Method Exception");
        }
    }
}