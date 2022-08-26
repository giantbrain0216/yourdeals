using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Shared.Services
{
    public  interface IRefrechUserTokenService
    {
        public int NumberOfExecution { get; }

        Task DoWork(CancellationToken CancellationToken);
    }
}
