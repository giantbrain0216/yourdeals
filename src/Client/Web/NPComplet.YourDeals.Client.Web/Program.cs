// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using NPComplet.YourDeals.Client.Shared.Services;
using NPComplet.YourDeals.Client.Shared.ViewModels.Communication;
using NPComplet.YourDeals.Client.Shared.ViewModels.Communication.Interfaces;
using NPComplet.YourDeals.Client.Shared.ViewModels.Constant;
using NPComplet.YourDeals.Client.Shared.ViewModels.Deals;
using NPComplet.YourDeals.Client.Shared.ViewModels.Deals.Interfaces;
using NPComplet.YourDeals.Client.Shared.ViewModels.Identity;
using NPComplet.YourDeals.Client.Shared.ViewModels.Shared;
using NPComplet.YourDeals.Client.Web.Services;

namespace NPComplet.YourDeals.Client.Web
{
    #region

    using Blazored.LocalStorage;
    using Blazored.Modal;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Logging;
    using NPComplet.YourDeals.Client.Shared.Helpers;
    using NPComplet.YourDeals.Client.Shared.Logic.Interfaces;
    using NPComplet.YourDeals.Client.Shared.ViewModels;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Base;

    using NPComplet.YourDeals.Client.Shared.WebUI;
    using NPComplet.YourDeals.Client.Web.AuthProviders;
    using NPComplet.YourDeals.Client.Web.Handlers;
    using NPComplet.YourDeals.Client.Web.Logic;
    using Radzen;
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Tewr.Blazor.FileReader;

    using NPComplet.YourDeals.Client.Shared.ViewModels.Shared.Interfaces;

    using NPComplet.YourDeals.Client.Web.Extensions;
    using NPComplet.YourDeals.Client.Web.WebViewModels;
    using Toolbelt.Blazor.Extensions.DependencyInjection;
    using NPComplet.YourDeals.Client.Shared.Logging;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Wallet;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Finance.FinanceSupports;
    using Microsoft.AspNetCore.Components.WebAssembly.Http;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Finance.FinanceOpreations.Payments.Interfaces;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Finance.FinanceOpreations.Payments;
    using NPComplet.YourDeals.Client.Shared.Services.BackgroudServices;
    using MvvmBlazor.Extensions;
    using Microsoft.MobileBlazorBindings;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Accounts.Invoice;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Accounts.Quotation;
    #endregion

    /// <summary>
    ///     The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args. 
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped<IFileProvider>(_ => new PhysicalFileProvider("/"));
           builder.Services.AddAntDesign();
            builder.Services.AddMvvm();

            builder.Services.AddBlazoredModal();
            builder.Services
                .AddScoped<AuthenticationStateProvider, AuthStateProvider>()
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IStorageService, WebStorageService>()
                .AddScoped<ForgetPasswordVM>()
                .AddScoped<LoginVM>()
                .AddScoped<GetProfileVM>()
                .AddBlazoredLocalStorage()
                .AddAuthorizationCore()
                .AddTransient<CustomAuthorizationHandler>()
                .AddScoped< CreateDealViewModel>()
                .AddScoped<ImageUploadViewModel>()
                .AddScoped<ChatHistoryViewModel>()
                .AddScoped<InvoiceViewModel>()
                .AddScoped<QuotationViewModel>()
                //.AddScoped<IChatHistoryViewModel, ChatHistoryViewModel>()
                //.AddScoped<ShellNavigationManager>()
                .AddScoped< DealPageViewModel>()
                .AddScoped<UserChat>()
                .AddScoped< DealsHistoryViewModel>()
                .AddScoped<ICultureSelectorViewModel, CultureSelectorViewModel>()
                .AddScoped<MapViewModel>()
                .AddSingleton<IBaseViewModel, BaseViewModel>()
                .AddScoped<AppShellViewModel>()
                .AddScoped< PropositionViewModel>()
                .AddScoped< PaymentsTransactionsViewModel>()
                .AddScoped< PaymentViewModel>()
                
                .AddScoped<UserWalletViewModel>().AddScoped<FinanceSupportViewModel>();

            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();

            builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);
            builder.Services.AddApiAuthorization();
           
            builder.Services.AddLocalization();
          
            var host = builder.Build();
            await host.SetDefaultCulture();
          
            builder.Services.AddSingleton<ApplicationLoggerProvider>();
           
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("YourDealsApi"))
                  
              .AddHttpClient("YourDealsApi", (serviceProvider, client) =>
              {
                
                  
                  client.DefaultRequestHeaders.AcceptLanguage.Clear();
                  client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(CultureInfo.DefaultThreadCurrentCulture?.TwoLetterISOLanguageName);
                  client.BaseAddress = new Uri(Config.BaseUrl);
                  client.EnableIntercept(serviceProvider);

              })
              .AddHttpMessageHandler<CustomAuthorizationHandler>();
           
            builder.Services.AddHttpClientInterceptor();
          
            builder.Services.AddSingleton<IHttpClientService, HttpClientService>();
            builder.Services.AddSingleton<HttpClientBackgroudService>();
            
            builder.Services.AddSingleton<RefrechUserTokenBackgroudServices>();
            builder.Services.AddSingleton<IRefrechUserTokenService, RefrechUserTokenService>();

        

            builder.Services.AddLogging(logging =>
            {
                var httpClient = builder.Services.BuildServiceProvider().GetRequiredService<HttpClient>();
                var authenticationStateProvider = builder.Services.BuildServiceProvider().GetRequiredService<AuthenticationStateProvider>();
                var applogger = builder.Services.BuildServiceProvider().GetRequiredService<ApplicationLoggerProvider>();

                logging.SetMinimumLevel(LogLevel.Trace);
                logging.AddProvider(applogger);
            });
          

            await builder.Build().RunAsync();

           
        }
    }
}