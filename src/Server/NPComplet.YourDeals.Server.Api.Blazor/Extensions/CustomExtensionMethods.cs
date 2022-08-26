// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomExtensionMethods.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Extensions
{
    #region

    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    using Invio.Extensions.Authentication.JwtBearer;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    using NPComplet.YourDeals.Domain.Shared.Constant.Application;
    using NPComplet.YourDeals.Domain.Shared.Constant.Permission;
    using NPComplet.YourDeals.Server.Api.Blazor.Extensions.Swagger;

    #endregion

    /// <summary>
    /// </summary>
    internal static class CustomExtensionMethods
    {
        /// <summary>
        /// The add custom api versioning.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/>.
        /// </returns>
        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(
                options =>
                    {
                        // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                        options.ReportApiVersions = true;

                        // Allows controllers that are not decorated with [ApiController] to be found
                        options.UseApiBehavior = false;
                    });
            services.AddVersionedApiExplorer(
                options =>
                    {
                        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                        // note: the specified format code will format the version as "'v'major[.minor][-status]"
                        options.GroupNameFormat = "'v'VVV";

                        options.SubstituteApiVersionInUrl = true;
                    });

            return services;
        }

        /// <summary>
        /// The add custom authentication.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        /// <param name="configuration">
        /// The configuration.
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/>.
        /// </returns>
        public static IServiceCollection AddCustomAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(
                authentication =>
                    {
                        authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }).AddJwtBearer(
                cfg =>
                    {
                        cfg.RequireHttpsMetadata = false;
                        cfg.SaveToken = true;
                        cfg.TokenValidationParameters = new TokenValidationParameters
                                                            {
                                                                ValidateIssuer = true,
                                                                ValidIssuer = configuration["Security:Tokens:Issuer"],
                                                                ValidateAudience = true,
                                                                ValidAudience =
                                                                    configuration["Security:Tokens:Audience"],
                                                                ValidateIssuerSigningKey = true,
                                                                IssuerSigningKey = new SymmetricSecurityKey(
                                                                    Encoding.UTF8.GetBytes(
                                                                        configuration["Security:Tokens:Key"]))
                                                            };
                        cfg.AddQueryStringAuthentication("access_token");
                        cfg.Events = new JwtBearerEvents
                                         {
                                             OnMessageReceived = context =>
                                                 {
                                                    
                                                    
                                                        var accessToken = context.Request.Headers["Authorization"];
                                                     
                                                     if (string.IsNullOrEmpty(accessToken))
                                                     {
                                                         accessToken = context.Request.Query["Authorization"];
                                                     }
                                                     var path = context.HttpContext.Request.Path;
                                                     
                                                     if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments(
                                                             ApplicationConstants.SignalR.HubUrlApi))
                                                     {
                                                         context.Token = accessToken.ToString().Replace("Bearer ", string.Empty)
                                .Replace(@"""", string.Empty).Replace(@"\", string.Empty); ;

                                                         var token = context.Request.Headers["Authorization"];
                                                         if (string.IsNullOrEmpty(token))
                                                         {
                                                             context.Request.Headers.Add(
                                                              "Authorization",
                                                              string.Format("Bearer {0}",accessToken));
                                                         }
                                                         
                                                     }

                                                     return Task.CompletedTask;
                                                 }
                                         };
                    });
            services.AddAuthorization(
                options =>
                    {
                        var defaultAuthorizationPolicyBuilder =
                            new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                        defaultAuthorizationPolicyBuilder =
                            defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                        options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();

                        foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
                        {
                            var propertyValue = prop.GetValue(null);
                            if (propertyValue is not null)
                            {
#pragma warning disable CS8604 // Possible null reference argument.
                                options.AddPolicy(propertyValue.ToString(), policy => policy.RequireClaim(ApplicationClaimTypes.Permission, propertyValue.ToString()));
#pragma warning restore CS8604 // Possible null reference argument.
                            }
                        }
                    });
            services.TryAddEnumerable(
                ServiceDescriptor.Singleton<IPostConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>());

            return services;
        }

        /// <summary>
        /// </summary>
        /// <param name="services">
        /// </param>
        /// <param name="configuration">
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/>.
        /// </returns>
        public static IServiceCollection AddCustomSwagger(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSwaggerGen(
                c =>
                    {
                        c.SwaggerDoc(
                            "v1",
                            new OpenApiInfo
                                {
                                    Version = "v1",
                                    Title = "Your RightnDeals API , assembly version :" + typeof(CustomExtensionMethods).Assembly.GetName().Version,
                                    Description = "this API provide",
                                    Contact = new OpenApiContact
                                                  {
                                                      Name = "Mouadh Jaber", Email = "mouadh.jaber@msn.com"
                                                  },
                                    License = new OpenApiLicense { Name = "Use under LICX" }
                                });
                        c.AddSecurityDefinition(
                            "Bearer",
                            new OpenApiSecurityScheme
                                {
                                    Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                                    Name = "Authorization",
                                    In = ParameterLocation.Header,
                                    Type = SecuritySchemeType.ApiKey,
                                    Scheme = "Bearer"
                                });
                        c.OperationFilter<SecurityRequirementsOperationFilter>();

                        c.OperationFilter<SwaggerLanguageHeader>();
                        // Set the comments path for the Swagger JSON and UI.
                        foreach (var name in Directory.GetFiles(
                            AppContext.BaseDirectory,
                            "*.XML",
                            SearchOption.AllDirectories))
                            c.IncludeXmlComments(name);
                        
                    });

            return services;
        }
    }
}