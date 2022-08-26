using System;
using System.Collections.Generic;
using System.Text;

namespace NPComplet.YourDeals.Client.Shared.Extensions
{
    using System.Globalization;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.JSInterop;
    using Microsoft.MobileBlazorBindings.ProtectedStorage;

    public static class ClientWebAssemblyHostExtension
    {
        public static async Task SetDefaultCulture(this IServiceProvider provider)
        {
            var localStorageService = provider.GetService<IProtectedStorage>();
            var result = await localStorageService?.GetAsync<string>("blazorCulture");
            var culture = result != null ? new CultureInfo(result) : new CultureInfo("en");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

       
    }
}
