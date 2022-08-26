// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthStateProvider.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Client.Web.AuthProviders
{
    using System;
    #region

    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Blazored.LocalStorage;

    using Microsoft.AspNetCore.Components.Authorization;

    using NPComplet.YourDeals.Client.Shared;
    using NPComplet.YourDeals.Client.Shared.Helpers;

    #endregion

    /// <summary>
    ///     The auth state provider.
    /// </summary>
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState _anonymous;

        private readonly HttpClient _httpClient;

        private readonly ILocalStorageService _localStorage;
      

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthStateProvider"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The http client. 
        /// </param>
        /// <param name="localStorage">
        /// The local storage. 
        /// </param>
        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this._httpClient = httpClient;
            this._localStorage = localStorage;
         
            this._anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///     The get authentication state async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var currentUser = await this.GetUserByJWTAsync();

            if (currentUser?.UserName != null)
            {
                // create a claims
                var otherClaims = new List<Claim> { new(ClaimTypes.Name, currentUser.UserName) };
                otherClaims.AddRange(currentUser.Claims.Select(newClaim => new Claim(newClaim.Key, newClaim.Value)));

                // create claimsIdentity and claimsPrincipal
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(otherClaims, "serverAuth")));
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        /// <summary>
        /// The get user by jwt async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<CurrentUser> GetUserByJWTAsync()
        {
            // pulling the token from localStorage
            var jwtToken = await this._localStorage.GetItemAsStringAsync("authToken");
            if (jwtToken == null) return null;
            var refretoken = await _localStorage.GetItemAsStringAsync("refreshToken");
            NPCompletApp.Token = jwtToken;
            NPCompletApp.RefreshToken = refretoken;
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            try
            {
              
                // making the http request
                var response = await this._httpClient.GetAsync(Config.CurrentUserInfo + "?Token=" + jwtToken);

                var responseStatusCode = response.StatusCode;
                if (responseStatusCode != HttpStatusCode.OK) return null;

                var returnedUser = await response.Content.ReadFromJsonAsync<CurrentUser>();
                return returnedUser != null ? await Task.FromResult(returnedUser) : null;
            }
            catch
            {
                return new CurrentUser();
            }
        }

        /// <summary>
        /// The notify user authentication.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        public void NotifyUserAuthentication(string email)
        {
            var authenticatedUser = new ClaimsPrincipal(
                new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "jwtAuthType"));

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            this.NotifyAuthenticationStateChanged(authState);
        }

        /// <summary>
        ///     The notify user logout.
        /// </summary>
        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(this._anonymous);
            this.NotifyAuthenticationStateChanged(authState);
        }
     
    }
}