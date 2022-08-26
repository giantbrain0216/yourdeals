using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.Exceptions;
using NPComplet.YourDeals.Domain.Shared.Monitoring;
using Radzen;

namespace NPComplet.YourDeals.Client.Shared.Logging
{
    public class DatabaseLogger : ILogger
    {
        private readonly HttpClient _httpClient;

        public AuthenticationStateProvider _authenticationStateProvider { get; }


#pragma warning disable S125 // Sections of code should not be commented out
        private readonly NotificationService _notificationService;
#pragma warning restore S125 // Sections of code should not be commented out
        public DatabaseLogger(IHttpClientService httpClient, NotificationService notficationService)

        {
            _httpClient = httpClient.ApplicationHttpClient;
#pragma warning disable S125 // Sections of code should not be commented out
            _notificationService = notficationService;
#pragma warning restore S125 // Sections of code should not be commented out

        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
           
            Task.Run(async () =>
            {

                Log log = new Log();
                log.Message = exception.Message;
                log.Exception = exception.StackTrace + " \n Source : " + exception.Source;
                log.Exception = log.Exception + "\n Inner exception  message :" + exception?.InnerException?.Message 
                + "\n Inner exception StackTrace : " + exception?.InnerException?.StackTrace + " \n Inner exception  Source : " + exception?.InnerException?.Source;

               await NPCompletApp.ShellViewModel.ShowNotification(NotificationSeverity.Error, "Error", exception.Message);
               await _httpClient.PostAsJsonAsync(Config.Logging, log);
            });

        }
    }
}
