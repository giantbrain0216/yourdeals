using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NPComplet.YourDeals.Client.Shared.Services
{
    public class HttpClientService : IHttpClientService
    {
        private  readonly HttpClient _applicationHttpClient;
        public HttpClient ApplicationHttpClient => _applicationHttpClient;

        public HttpClientService(HttpClient httpClient)
        {
            _applicationHttpClient = httpClient;
        }

        
    }
}
