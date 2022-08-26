// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigureJwtBearerOptions.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Extensions
{
    #region

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Options;

    #endregion

    /// <summary>
    /// </summary>
    public class ConfigureJwtBearerOptions : IPostConfigureOptions<JwtBearerOptions>
    {
        /// <summary>
        /// </summary>
        /// <param name="name">
        /// </param>
        /// <param name="options">
        /// </param>
        public void PostConfigure(string name, JwtBearerOptions options)
        {
            var originalOnMessageReceived = options.Events.OnMessageReceived;
            options.Events.OnMessageReceived = async context =>
                {
                    await originalOnMessageReceived(context);

                    if (string.IsNullOrEmpty(context.Token))
                    {
                        
                            var accessToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty)
                                .Replace(@"""", string.Empty).Replace(@"\", string.Empty);
                            var path = context.HttpContext.Request.Path;

                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/signalRHub"))
                                context.Token = accessToken;
                      

                    }
                };
        }



    }
}