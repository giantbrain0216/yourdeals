// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityRequirementsOperationFilter.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Extensions.Swagger
{
    #region

    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.OpenApi.Models;

    using Swashbuckle.AspNetCore.SwaggerGen;

    #endregion

    #endregion

    /// <summary>
    /// </summary>
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        /// <summary>
        /// </summary>
        /// <param name="operation">
        /// </param>
        /// <param name="context">
        /// </param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context.MethodInfo.DeclaringType == null
                                     ? null
                                     : context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                                         .Union(context.MethodInfo.GetCustomAttributes(true))
                                         .OfType<AuthorizeAttribute>();

            var anonymousAttributes = context.MethodInfo.DeclaringType == null
                                          ? null
                                          : context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                                              .Union(context.MethodInfo.GetCustomAttributes(true))
                                              .OfType<AllowAnonymousAttribute>();
            if (authAttributes != null && anonymousAttributes != null)
                if (authAttributes.Any() && !anonymousAttributes.Any())
                {
                    operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                    var oAuthScheme = new OpenApiSecurityScheme
                                          {
                                              Reference = new OpenApiReference
                                                              {
                                                                  Type = ReferenceType.SecurityScheme, Id = "Bearer"
                                                              }
                                          };

                    operation.Security = new List<OpenApiSecurityRequirement>();
                    operation.Security.Add(new OpenApiSecurityRequirement { { oAuthScheme, Array.Empty<string>() } });
                }
        }
    }
}