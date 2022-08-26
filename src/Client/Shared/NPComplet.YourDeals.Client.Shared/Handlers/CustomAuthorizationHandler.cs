using Microsoft.MobileBlazorBindings.ProtectedStorage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Shared.Handlers
{
    public class CustomAuthorizationHandler : DelegatingHandler
    {
        public IProtectedStorage _localStorageService { get; set; }
        public CustomAuthorizationHandler(IProtectedStorage localStorageService)
        {
            //injecting local storage service
            _localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //getting token from the localstorage
          

            //adding the token in authorization header
            if (NPCompletApp.Token != null)
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", NPCompletApp.Token);
            try
            {
                //sending the request
                return await base.SendAsync(request, cancellationToken);
            }
            catch
            {
                return await base.SendAsync(request, new CancellationToken());
            }
        }
    }
}
