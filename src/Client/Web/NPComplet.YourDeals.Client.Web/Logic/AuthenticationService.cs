// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthenticationService.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Client.Web.Logic
{
    using System;
    using System.Collections.Generic;
    #region

    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Blazored.LocalStorage;

    using Microsoft.AspNetCore.Components.Authorization;
    using NPComplet.YourDeals.Client.Shared;
    using NPComplet.YourDeals.Client.Shared.Helpers;
    using NPComplet.YourDeals.Client.Shared.Logging;
    using NPComplet.YourDeals.Client.Shared.Logic.Interfaces;
    using NPComplet.YourDeals.Client.Web.AuthProviders;
    using NPComplet.YourDeals.Domain.Shared.Constant.Storage;
    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects;
    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.ExternalLogins;
    using NPComplet.YourDeals.Domain.Shared.Wrapper;

    #endregion

    /// <summary>
    ///     The authentication service.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authStateProvider;

        private readonly HttpClient _client;

        private readonly ILocalStorageService _localStorage;

        private readonly JsonSerializerOptions _options;
       

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationService"/> class.
        /// </summary>
        /// <param name="client">
        /// The client. 
        /// </param>
        /// <param name="authStateProvider">
        /// The auth state provider. 
        /// </param>
        /// <param name="localStorage">
        /// The local storage. 
        /// </param>
        public AuthenticationService(
            HttpClient client,
            AuthenticationStateProvider authStateProvider,
            ILocalStorageService localStorage)
        {
            this._client = client;
            this._options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            this._authStateProvider = authStateProvider;
            this._localStorage = localStorage;
           
        }

        /// <summary>
        /// Confirm email.
        /// </summary>
        /// <param name="confirmationBody">
        /// The confirmation body.
        /// </param>
        /// <returns>
        /// True, if confirmed, otherwise false.
        /// </returns>
        public async Task<bool> ConfirmEmail(ConfirmationBody confirmationBody)
        {
            var content = JsonSerializer.Serialize(confirmationBody);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            
            var authResult = await this._client.PostAsync(Config.ConfirmEmail, bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<bool>(authContent, this._options);
        }

        /// <summary>
        /// The login.
        /// </summary>
        /// <param name="userForAuthentication">
        /// The user for authentication. 
        /// </param>
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

            await _localStorage.SetItemAsync("authToken", result.Data.Token);
            await _localStorage.SetItemAsync(StorageConstants.Local.RefreshToken, result.Data.RefreshToken);
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

            await _localStorage.SetItemAsync("authToken", result.Data.Token);
            await _localStorage.SetItemAsync(StorageConstants.Local.RefreshToken, result.Data.RefreshToken);
            //((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthenticationnExternal.Email);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Data.Token);
            NPCompletApp.ShellViewModel.isLoggedIn = true;

            return result;
        }

        public async Task<Result<string>> GetFacebookAppId()
        {

            var result =  await _client.GetAsync(Config.GetFacebookAppId + "?Token=");
            var content = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Result<string>>(content, _options);
        }

        public async Task<Result<string>> GetGoogleAppId()
        {

            var result = await _client.GetAsync(Config.GetGoogleAppId + "?Token=");
            var content = await result.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Result<string>>(content, _options);
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

        public async Task<Result<AuthResponseDto>> RegisterWithExternalProvider(ValidateAccessTokenDto validateAccessTokenDto)
        {
            var content = JsonSerializer.Serialize(validateAccessTokenDto);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");


            await _client.PostAsync(Config.RegisterWithExternalProvider, bodyContent);
            
            ExternalLoginViewModel externalLoginViewModel = new ExternalLoginViewModel()
            { 
                ExternalId = validateAccessTokenDto.AuterizationCode,
                Email = validateAccessTokenDto.Email,
                FistName = validateAccessTokenDto.FirstName,
                LastName = validateAccessTokenDto.LastName,
                Provider = validateAccessTokenDto.Provider
            };

            return await LoginWithExternalProvider(externalLoginViewModel);


        }

        /// <summary>
        ///     The logout.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public async Task Logout()
        {
            await this._localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync(StorageConstants.Local.RefreshToken);
            ((AuthStateProvider)this._authStateProvider).NotifyUserLogout();
            this._client.DefaultRequestHeaders.Authorization = null;
        }

        /// <summary>
        /// </summary>
        /// <param name="userForAuthentication">
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<RegistrationResponseDto> Register(RegisterViewModel userForAuthentication)
        {
          
            var authResult = await this._client.PostAsync(
                                 Config.Register,
                                 new StringContent(
                                     JsonSerializer.Serialize(userForAuthentication),
                                     Encoding.UTF8,
                                     "application/json"));

            return JsonSerializer.Deserialize<RegistrationResponseDto>(
                await authResult.Content.ReadAsStringAsync(),
                this._options);
        }

        /// <summary>
        /// The resend verification.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task ResendVerification(string id, string RedirectUrl)
        {
            
            await this._client.GetAsync($"{Config.ReSendVerificationEmail}/{id}?RedirectUrl=" + RedirectUrl +"");
        }
       

        

        public async Task<Result<AuthResponseDto>> SendVerificationEmailByEmail(ForgotPasswordViewModel UserForForgotPassword)
        {
            Result<AuthResponseDto> result = new Result<AuthResponseDto>();
            var content = JsonSerializer.Serialize(UserForForgotPassword);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
           
            var forgotPasswordResult = await _client.PostAsync(Config.SendVerificationEmail, bodyContent);
            if (!forgotPasswordResult.IsSuccessStatusCode)
            {
                result = new Result<AuthResponseDto> { Succeeded=false,Messages=new List<string> { "Failed to send verification mail due to error."} };
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
        public async Task<Result<AuthResponseDto>> ResetPassword(ForgotPasswordViewModel userForForgotPassword, string userId)
        {
            var resetPasswordResult = await _client.GetAsync($"{Config.ResetPassword}?userId={userId}&password={userForForgotPassword.NewPassword}&token={userForForgotPassword.CopiedToken}");
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

     
    }
}