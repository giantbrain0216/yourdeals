using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Authentication;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Web.Admin
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args).AddRootComponents()
                          .AddClientServices();

            var host = builder.Build();
            await host.SetDefaultCulture();
            await builder.Build().RunAsync();
        }
    }
}
