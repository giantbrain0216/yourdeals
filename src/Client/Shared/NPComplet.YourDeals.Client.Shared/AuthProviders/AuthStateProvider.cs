// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthStateProvider.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Client.Shared.AuthProviders
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

    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.MobileBlazorBindings.ProtectedStorage;

    using NPComplet.YourDeals.Client.Shared.Helpers;
    using NPComplet.YourDeals.Client.Shared.Logging;

    #endregion

    /// <summary>
    ///     The auth state provider.
    /// </summary>
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly AuthenticationState _anonymous;

        private readonly HttpClient _httpClient;

        private readonly IProtectedStorage _localStorage;
     
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthStateProvider"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        /// <param name="localStorage">
        /// The local storage.
        /// </param>
        public AuthStateProvider(IHttpClientService httpClient, IProtectedStorage localStorage)
        {
            this._httpClient = httpClient.ApplicationHttpClient;
            this._localStorage = localStorage;
          
            this._anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        /// <summary>
        ///     Gets or sets the token.
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
                var otherClaims = new List<Claim>
                                      {
                                          new Claim(
                                              ClaimTypes.Name,
                                              currentUser.Claims.SingleOrDefault(p => p.Key.Contains("emailaddress"))
                                                  .Value)
                                      };
                otherClaims.AddRange(currentUser.Claims.Select(newClaim => new Claim(newClaim.Key, newClaim.Value)));

                // create claimsIdentity
                var claimsIdentity = new ClaimsIdentity(otherClaims, "serverAuth");

                // create claimsPrincipal
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                return new AuthenticationState(claimsPrincipal);
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        /// <summary>
        ///     The get user by jwt async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public async Task<CurrentUser> GetUserByJWTAsync()
        {
            // pulling the token from localStorage
            var jwtToken = await this._localStorage.GetAsync<string>("authToken");
            if (jwtToken == null) return null;
          var refretoken=  await _localStorage.GetAsync<string>("refreshToken");
            NPCompletApp.Token = jwtToken;
            NPCompletApp.RefreshToken = refretoken;
            this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            // making the http request
            var response = await this._httpClient.GetAsync(Config.CurrentUserInfo+"?Token="+ jwtToken);

            if (response.StatusCode != HttpStatusCode.OK) return null;

            var returnedUser = await response.Content.ReadFromJsonAsync<CurrentUser>();

            return returnedUser != null ? await Task.FromResult(returnedUser) : null;
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

            this.NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));
        }

        /// <summary>
        ///     The notify user logout.
        /// </summary>
        public void NotifyUserLogout()
        {
            this.NotifyAuthenticationStateChanged(Task.FromResult(this._anonymous));
        }
       
    }
}