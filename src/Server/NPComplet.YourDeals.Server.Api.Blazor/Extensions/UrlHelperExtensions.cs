// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrlHelperExtensions.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Extensions
{
    #region

    using System;
    using System.Web;

    using Microsoft.AspNetCore.Mvc;

    using NPComplet.YourDeals.Server.Api.Blazor.Controllers.Users;

    #endregion

    /// <summary>
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="urlHelper">
        /// </param>
        /// <param name="userId">
        /// </param>
        /// <param name="password">
        /// </param>
        /// <param name="code">
        /// </param>
        /// <param name="scheme">
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ResetPasswordCallbackLink(
            this IUrlHelper urlHelper,
            Guid userId,
            string password,
            string code,
            string scheme)
        {
            return urlHelper.Action(
                nameof(AccountsController.ResetPassword),
                "Accounts",
                new { userId, password, codedversion = HttpUtility.UrlEncode(code) },
                scheme);
        }
    }
}