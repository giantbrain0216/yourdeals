// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAuthorizationHandler.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Client.Web.Handlers
{
    using System.Collections.Generic;
    #region

    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;

    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.WebAssembly.Http;

    #endregion

    /// <summary>
    /// The custom authorization handler.
    /// </summary>
    public class CustomAuthorizationHandler : DelegatingHandler
    {
     
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAuthorizationHandler"/> class.
        /// </summary>
        /// <param name="localStorageService">
        /// The local storage service.
        /// </param>
        public CustomAuthorizationHandler(ILocalStorageService localStorageService)
        {
            // injecting local storage service
            this._localStorageService = localStorageService;
          

        }
       
        /// <summary>
        /// Gets or sets the _local storage service.
        /// </summary>
        public ILocalStorageService _localStorageService { get; set; }

        /// <summary>
        /// The send async.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="cancellationToken">
        /// The cancellation token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // getting token from the localstorage
            var jwtToken = await this._localStorageService.GetItemAsync<string>("authToken");

            // adding the token in authorization header
            if (jwtToken != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);


            // sending the request
            return await base.SendAsync(request, cancellationToken);
            //return await base.SendAsync(request, cancellationToken);
            
        }
    }
}