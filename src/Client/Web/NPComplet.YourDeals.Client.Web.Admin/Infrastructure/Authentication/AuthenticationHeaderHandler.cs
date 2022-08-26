using Blazored.LocalStorage;
using NPComplet.YourDeals.Domain.Shared.Constant.Storage;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Authentication
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public AuthenticationHeaderHandler(ILocalStorageService localStorage)
        {
            this._localStorage = localStorage;

        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            
                var savedToken = await this._localStorage.GetItemAsync<string>(StorageConstants.Local.AuthToken);

                if (!string.IsNullOrEmpty(savedToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
               
                }
            

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
