using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.MobileBlazorBindings.ProtectedStorage;
using NPComplet.YourDeals.Client.Shared.Helpers;
using NPComplet.YourDeals.Client.Shared.ViewModels.Constant;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using Radzen;
using Toolbelt.Blazor;

namespace NPComplet.YourDeals.Client.Shared.Services.BackgroudServices
{
    public class RefrechUserTokenBackgroudServices : BackgroudServiceWASM
    {
        private IRefrechUserTokenService _refrechUserTokenService;
        private readonly HttpClientInterceptor _interceptor;

        private CancellationToken CancellationToken;

        public RefrechUserTokenBackgroudServices(IRefrechUserTokenService refrechUserTokenService,
            HttpClientInterceptor interceptor) : base()
        {
            _refrechUserTokenService = refrechUserTokenService;

            _interceptor = interceptor;
        }



        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            CancellationToken = stoppingToken;

            _interceptor.BeforeSend += InterceptResponse;
        }

        protected async override Task StopAsync(CancellationToken cancellationToken)
        {
            CancellationToken = cancellationToken;
            _interceptor.BeforeSend -= InterceptResponse;
        }


        private async void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            await _refrechUserTokenService.DoWork(CancellationToken);
        }
    }
}
