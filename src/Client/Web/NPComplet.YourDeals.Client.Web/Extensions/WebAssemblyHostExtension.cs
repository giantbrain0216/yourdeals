using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Web.Extensions
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.JSInterop;

    public static class WebAssemblyHostExtension
    {
        public async static Task SetDefaultCulture(this WebAssemblyHost provider)
        {
            var localStorageService = provider.Services.GetRequiredService<ILocalStorageService>();
            var result = await localStorageService.GetItemAsync<string>("blazorCulture");
            CultureInfo culture;
            if (result != null)
                culture = new CultureInfo(result);
            else
                culture = new CultureInfo("en");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
      
    }
}
