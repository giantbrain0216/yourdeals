using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace NPComplet.YourDeals.Client.Shared.Services
{
    public  interface IHttpClientService
    {

        public HttpClient ApplicationHttpClient { get;  }


    }
}
