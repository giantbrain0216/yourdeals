using Microsoft.Extensions.Hosting;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Radzen;
using Toolbelt.Blazor;
using NPComplet.YourDeals.Client.Shared.ViewModels.Constant;
using System;

namespace NPComplet.YourDeals.Client.Shared.Services.BackgroudServices
{
    public class HttpClientBackgroudService : BackgroudServiceWASM
    {
        private IHttpClientService _httpClientService;

        private readonly HttpClientInterceptor _interceptor;

        private readonly NotificationService _notificationService;



        public HttpClientBackgroudService(IHttpClientService httpClientService, HttpClientInterceptor interceptor) : base()
        {
            _httpClientService = httpClientService;
            _interceptor = interceptor;

        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _interceptor.AfterSend += InterceptResponse;
        }

        protected async override Task StopAsync(CancellationToken cancellationToken)
        {
            _interceptor.AfterSend -= InterceptResponse;

        }


        private async void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            try
            {
                if (e.Response.IsSuccessStatusCode) return;
                var responseCode = e.Response.StatusCode;

                string message;
                switch (responseCode)
                {
                    case HttpStatusCode.NotFound:
                        message = "The requested resorce was not found.";
                        await NPCompletApp.ShellViewModel.ShowNotification(NotificationSeverity.Error, "Error ", message);
                        break;
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.Forbidden:

                        message = "You are not authorized to access this resource. ";
                        await NPCompletApp.ShellViewModel.ShowNotification(NotificationSeverity.Error, "Error ", message);
                        break;
                    default:
                        message = e.Response.ReasonPhrase;
                        await NPCompletApp.ShellViewModel.ShowNotification(NotificationSeverity.Error, "Error ", message);
                        break;

                }
            }
            catch (Exception ex)
            {
                await NPCompletApp.ShellViewModel.ShowNotification(NotificationSeverity.Error, "Error ", ex.Message);
            }

        }
    }
}
