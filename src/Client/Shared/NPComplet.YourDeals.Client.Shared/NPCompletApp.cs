// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NPCompletApp.cs" company="NPComplet">
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

namespace NPComplet.YourDeals.Client.Shared
{
    using AntDesign;
    #region

    using Blazored.Modal;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Logging;
    using Microsoft.MobileBlazorBindings;
    using MvvmBlazor.Extensions;
    using NPComplet.YourDeals.Client.Shared.AuthProviders;
    using NPComplet.YourDeals.Client.Shared.Extensions;
    using NPComplet.YourDeals.Client.Shared.Handlers;
    using NPComplet.YourDeals.Client.Shared.Helpers;
    using NPComplet.YourDeals.Client.Shared.Logging;
    using NPComplet.YourDeals.Client.Shared.Logic;
    using NPComplet.YourDeals.Client.Shared.Logic.Interfaces;
    using NPComplet.YourDeals.Client.Shared.Services.BackgroudServices;
    using NPComplet.YourDeals.Client.Shared.ViewModels;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Accounts.Invoice;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Accounts.Quotation;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Base;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Finance.FinanceOpreations.Payments;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Finance.FinanceOpreations.Payments.Interfaces;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Finance.FinanceSupports;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Shared;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Shared.Interfaces;
    using NPComplet.YourDeals.Client.Shared.ViewModels.Wallet;
    using Radzen;
    using System;
    using System.Globalization;
    using System.Net.Http;
    using Tewr.Blazor.FileReader;
    using Toolbelt.Blazor.Extensions.DependencyInjection;
    using Xamarin.Forms;
    using Shell = Microsoft.MobileBlazorBindings.Elements.Shell;

    #endregion

    /// <summary>
    /// The np complet app.
    /// </summary>
    public class NPCompletApp : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NPCompletApp"/> class.
        /// </summary>
        /// <param name="fileProvider">
        /// The file provider.
        /// </param>
        public NPCompletApp(IFileProvider fileProvider = null)
        {
            var hostBuilder = MobileBlazorBindingsHost.CreateDefaultBuilder().ConfigureServices(
                (hostContext, services) =>
                    {
                        // Adds web-specific services such as NavigationManager
                        services.AddBlazorHybrid();
                        services.AddBlazoredModal();
                        services.AddAntDesign();
                        services.AddMvvm();
                        services.AddSingleton<ShellNavigationManager>();
                        services.AddScoped<MapViewModel>();
                        services.AddTransient<CustomAuthorizationHandler>();

                        services.AddProtectedStorage();
                        services.AddAuthorizationCore(
                            options => options.AddPolicy(
                                "IsAuthenticated",
                                policy => policy.RequireClaim("IsAuthenticated")));
                        services.AddFileReaderService(o => o.UseWasmSharedBuffer = false);

                        // Register app-specific services
                        services
                            .AddScoped<AuthenticationStateProvider, AuthStateProvider>()
                            .AddScoped<IStorageService, StorageService>()
                            .AddScoped<ImageUploadViewModel>()
                            .AddScoped<UserChat>()
                            .AddScoped<AppShellViewModel>()
                            .AddScoped<ForgetPasswordVM>()
                            .AddScoped<LoginVM>()
                            //.AddScoped<GetProfileVM>()
                            
                            .AddScoped<MobileCurrentLocationViewModel>()
                            .AddScoped<IAuthenticationService, AuthenticationService>()
                            .AddSingleton<IBaseViewModel, BaseViewModel>()
                            .AddScoped<ICultureSelectorViewModel, CultureSelectorViewModel>()
                            .AddScoped<CreateDealViewModel>()
                            .AddScoped<ChatHistoryViewModel>()
                            //.AddScoped<IChatHistoryViewModel,ChatHistoryViewModel>()
                            .AddScoped<DealPageViewModel>()
                            .AddScoped<DealsHistoryViewModel>()
                            //.AddScoped<ShellNavigationManager>()
                            .AddScoped<PropositionViewModel>()
                            .AddScoped<PaymentsTransactionsViewModel>()
                            .AddScoped<InvoiceViewModel>()
                            .AddScoped<QuotationViewModel>()
                            .AddScoped<PaymentViewModel>()
                            .AddScoped<UserWalletViewModel>().AddScoped<FinanceSupportViewModel>();

                        services.AddScoped<DialogService>();
                        services.AddScoped<Radzen.NotificationService>();
                        services.AddScoped<AntDesign.NotificationService>();

                        services.AddScoped<TooltipService>();
                        services.AddScoped<ContextMenuService>();

                        services.AddLocalization();

                        _ = services.BuildServiceProvider().SetDefaultCulture();
                        services.AddSingleton<ApplicationLoggerProvider>();
                        services.AddScoped(sp => sp
             .GetRequiredService<IHttpClientFactory>()
             .CreateClient("YourDealsApi").EnableIntercept(sp))
         .AddHttpClient("YourDealsApi", client =>
         {
             client.DefaultRequestHeaders.AcceptLanguage.Clear();
             client.DefaultRequestHeaders.AcceptLanguage.ParseAdd(CultureInfo.DefaultThreadCurrentCulture?.TwoLetterISOLanguageName);
             client.BaseAddress = new Uri(Config.BaseUrl);
         })
         .AddHttpMessageHandler<CustomAuthorizationHandler>();
                        services.AddHttpClientInterceptor();


                       
                        services.AddSingleton<HttpClientBackgroudService>();
                        services.AddSingleton<IHttpClientService, HttpClientService>();
                        services.AddSingleton<RefrechUserTokenBackgroudServices>();
                        services.AddSingleton<IRefrechUserTokenService, RefrechUserTokenService>();
                        services.AddLogging(logging =>
                        {
                            var httpClient = services.BuildServiceProvider().GetRequiredService<HttpClient>();
                            var authenticationStateProvider = services.BuildServiceProvider().GetRequiredService<AuthenticationStateProvider>();
                            var applogger = services.BuildServiceProvider().GetRequiredService<ApplicationLoggerProvider>();

                            logging.SetMinimumLevel(LogLevel.Error);
                            logging.AddProvider(applogger);
                        });
                    }).UseWebRoot("wwwroot");

            if (fileProvider != null) hostBuilder.UseStaticFiles(fileProvider);
            else hostBuilder.UseStaticFiles();



            var host = hostBuilder.Build();

            if (Device.RuntimePlatform == Device.WPF)
            {
                this.MainPage = new ContentPage { Title = string.Empty };
                NavigationPage.SetHasNavigationBar(this.MainPage, false);
                host.AddComponent<Main>(this.MainPage);
            }
            else
            {
                this.MainPage = new ContentPage();
                host.AddComponent<Main>(this);
            }
        }

        /// <summary>
        /// Gets or sets the my shell.
        /// </summary>
        public static Shell MyShell { get; set; }

        /// <summary>
        /// Gets or sets the shell view model.
        /// </summary>
        public static AppShellViewModel ShellViewModel { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public static string Token { get; set; }
        public static string RefreshToken { get; set; }
        /// <summary>
        /// The on resume.
        /// </summary>
        protected override void OnResume()
        {
            // When is Application on Resume
        }

        /// <summary>
        /// The on sleep.
        /// </summary>
        protected override void OnSleep()
        {
            // When is Application on OnSleep
        }

        /// <summary>
        /// The on start.
        /// </summary>
        protected override void OnStart()
        {
            // When is Application on Start
        }
    }
}