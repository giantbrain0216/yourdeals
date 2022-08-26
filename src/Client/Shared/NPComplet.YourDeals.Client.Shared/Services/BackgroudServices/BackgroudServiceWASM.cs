using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using NPComplet.YourDeals.Client.Shared.ViewModels.Constant;

namespace NPComplet.YourDeals.Client.Shared.Services.BackgroudServices
{
    public abstract class BackgroudServiceWASM : IDisposable
    {
        private   CancellationTokenSource source ;
        private readonly CancellationToken _cancellationToken ;

        private volatile bool _IsRuuning;

        public BackgroudServiceWASM()
        {
            source = new CancellationTokenSource();
            _cancellationToken = source.Token;

          
        }

        public async Task StartWorking()
        {
            if (!_IsRuuning)
            {
                await Task.Run(async () => await ExecuteAsync(_cancellationToken));
                _IsRuuning = true;
            }
         
        }

        public async Task StopWorking()
        {
            if (_IsRuuning)
            {
                source.Cancel();
                await Task.Run(async () => await StopAsync(_cancellationToken));
                _IsRuuning = false;
            }
        }

        protected   abstract  Task ExecuteAsync(CancellationToken stoppingToken);

        protected abstract Task StopAsync(CancellationToken cancellationToken);


        public void Dispose()
        {
            source.Cancel();
            Task.Run(async () => await StopAsync(_cancellationToken));
        }
    }
}
