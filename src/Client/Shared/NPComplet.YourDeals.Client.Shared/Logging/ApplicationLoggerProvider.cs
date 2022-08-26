using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Radzen;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using NPComplet.YourDeals.Client.Shared.Services;

namespace NPComplet.YourDeals.Client.Shared.Logging
{

    public sealed class ApplicationLoggerProvider : ILoggerProvider
    {
        private readonly IHttpClientService _httpClient;

        private readonly NotificationService _notficationService;

        public ApplicationLoggerProvider(IHttpClientService httpClient,NotificationService notficationService)
        {
            _httpClient = httpClient;
            _notficationService = notficationService;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DatabaseLogger(_httpClient, _notficationService);
        }


        void IDisposable.Dispose()
        {
            // Method intentionally left empty.
      
        }
        
    }
}
