// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using NPComplet.YourDeals.Server.Infrastructure.Services.Classes.Communication;

namespace NPComplet.YourDeals.Server.Api.Blazor
{
    #region

    using Controllers.Admin.Services.Identity;
    using Controllers.Admin.Services.Identity.Interfaces;
    using Domain.Shared.Constant.Application;
    using Domain.Shared.Users;
    using Extensions;
    using Filters;
    using Hangfire;
    using Hubs;
    using Infrastructure.RazorHtmlEmails.Service;
    using Infrastructure.Repositories.Classes;
    using Infrastructure.Repositories.Data;
    using Infrastructure.Repositories.Interfaces;
    using Infrastructure.Services.Classes;
    using Infrastructure.Services.ConfigurationSettings;
    using Infrastructure.Services.Interfaces;
    using Init;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Localization.Routing;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.AspNetCore.Rewrite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.WebSockets;
    using System.Threading;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">
        /// </param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// The configure.
        /// </summary>
        /// <param name="app">
        /// The app.
        /// </param>
        /// <param name="serviceProvider">
        /// The service provider.
        /// </param>
        /// <param name="loggerFactory">
        /// The logger factory.
        /// </param>
        public void Configure(
            IApplicationBuilder app,
            IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();
            app.UseHsts();
            app.UseRouting();
 
            app.UseCors("CorsPolicy");
            app.UseFileServer();
            app.UseSwagger(options => { options.RouteTemplate = "docs/{documentName}/docs.json"; });

            app.UseSwaggerUI(
                options =>
                    {
                        options.SwaggerEndpoint("/docs/v1/docs.json", "My API V1");

                        options.RoutePrefix = "docs";

                        options.InjectStylesheet("/swagger-custom/swagger-custom-styles.css");
                        options.InjectJavascript("/swagger-custom/swagger-custom-script.js");
                    });

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                DashboardTitle = "NPComplet Jobs",
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });
            app.UseRequestLocalization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllers();
                        endpoints.MapHub<SignalRHub>("/signalRHub", _ => { });
                        endpoints.MapHub<DealHub>("/dealHub", _ => { });
                    });

            loggerFactory.AddLog4Net();

            var webSocketOptions = new WebSocketOptions();
            webSocketOptions.AllowedOrigins.Add("http://rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("https://rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("http://wwww.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("https://wwww.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("http://uat.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("https://uat.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("http://wwww.uat.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("https://wwww.uat.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("http://webadmin.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("https://webadmin.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("http://wwww.webadmin.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("https://wwww.webadmin.rightndeals.com");
            app.UseWebSockets(webSocketOptions);
     
            app.UseStaticFiles();
            app.Use(
                async (context, next) =>
                    {
                        if (context.Request.Path == "/" + ApplicationConstants.SignalR.HubUrl)
                        {
                            if (context.WebSockets.IsWebSocketRequest)
                                using (WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync())
                                {
                                    await Echo(webSocket);
                                }
                            else context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        else
                        {
                            await next();
                        }
                    });            //var webSocketOptions = new WebSocketOptions { KeepAliveInterval = TimeSpan.FromSeconds(120) };
            webSocketOptions.AllowedOrigins.Add("http://rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("https://rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("http://wwww.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("https://wwww.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("http://uat.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("https://uat.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("http://wwww.uat.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("https://wwww.uat.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("http://webadmin.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("https://webadmin.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("http://wwww.webadmin.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("https://wwww.webadmin.rightndeals.com");
            webSocketOptions.AllowedOrigins.Add("https://localhost:4356");
            app.UseWebSockets(webSocketOptions);
            app.Use(
                async (context, next) =>
                    {
                        if (context.Request.Path == "/" + ApplicationConstants.SignalR.HubUrl)
                        {
                            if (context.WebSockets.IsWebSocketRequest)
                                using (WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync())
                                {
                                    await Echo(webSocket);
                                }
                            else context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        else
                        {
                            await next();
                        }
                    });

            app.UseRewriter(new RewriteOptions().AddRedirect("^$", "docs"));
            app.UseMiddleware<ExceptionMiddleware>();
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    try
                    {
                        context?.Database.Migrate();
                    }
                    catch (Exception)
                    {
                        // Ignore
                    }

                    try
                    {
                        SeedDataInDatabase.SeedData(serviceProvider).Wait();
                    }
                    catch (Exception)
                    {
                        // Ignore
                    }
                }
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// <summary>
        /// </summary>
        /// <param name="services">
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(
                options =>
                    {
                        options.AddPolicy(
                            "CorsPolicy",
                            builder => builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
                    });
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            }).AddJsonProtocol();
            services.AddCustomAuthentication(Configuration);

           
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration["ConnectionString:SQLConnectionString"]));
            services.AddHangfireServer();
            // Configure the database connection 
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration["ConnectionString:SQLConnectionString"]));

            services.AddIdentity<User, Role>(
                options =>
                    {
                        options.SignIn.RequireConfirmedEmail = true;

                        // Default Password settings.
                        options.Password.RequireDigit = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 1;
                        options.Password.RequiredUniqueChars = 0;
                        options.User.AllowedUserNameCharacters = null;
                    }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(
                options =>
                    {
                        options.TokenLifespan = TimeSpan.FromSeconds(
                            Convert.ToDouble(Configuration["Security:Tokens:ExpirationSecondsInteger"]));
                    });


            services.AddHttpClient();


            // Add application services.
            services.Configure<EmailSettings>(options => Configuration.GetSection("EmailSettings").Bind(options));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
            services.AddScoped<IExcelService, ExcelService>();
            services.AddScoped<IExcelService, ExcelService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
            services.AddScoped<IFinanceService, FinanceService>();
            services.AddScoped<IFinanceOperationService, FinanceOperationService>();
            services.AddScoped<ICyberSorurceFiananceGateWay, CyberSourceFiananceGateWay>();
            services.AddScoped<IUserEarningsWalletService, UserEarningsWalletService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddSingleton(Configuration);
            services.AddRazorPages();
            services.AddControllers();
            services.AddCustomApiVersioning();

            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddSingleton(typeof(IBackgroundJobService), typeof(BackgroundJobService));

            services.AddCustomSwagger(Configuration);
            services.AddServerSideBlazor();
            //services.AddMailKit(config => config.UseMailKit());
            services.AddResponseCompression(
                opts =>
                    {
                        opts.MimeTypes =
                            ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
                    });



            services.Configure<RequestLocalizationOptions>(
                opts =>
                    {
                        var supportedCultures = new List<CultureInfo> { new("ar"), new("fr"), new("en") };
                        opts.DefaultRequestCulture = new RequestCulture("en", "en");
                        opts.SupportedCultures = supportedCultures;
                        opts.SupportedUICultures = supportedCultures;
                        opts.RequestCultureProviders.Clear();

                        opts.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider());
                        opts.RequestCultureProviders.Insert(1, new QueryStringRequestCultureProvider());
                        opts.RequestCultureProviders.Insert(2, new CookieRequestCultureProvider());
                        opts.RequestCultureProviders.Insert(3, new AcceptLanguageHeaderRequestCultureProvider());
                        services.AddSingleton(opts);
                    });
            services.AddTransient<IRoleClaimService, RoleClaimService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IUserService, UserService>();



            #region MVC
            services.AddHttpContextAccessor();
            services.AddMvc()
                .AddNewtonsoftJson(options => {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; } )
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            #endregion
        }

        /// <summary>
        /// </summary>
        /// <param name="webSocket">
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private static async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(
                                                new ArraySegment<byte>(buffer),
                                                CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, result.Count),
                    result.MessageType,
                    result.EndOfMessage,
                    CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}