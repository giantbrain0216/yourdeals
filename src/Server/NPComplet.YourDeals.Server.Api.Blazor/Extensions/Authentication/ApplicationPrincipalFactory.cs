// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationPrincipalFactory.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Extensions.Authentication
{
    #region

    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;

    using NPComplet.YourDeals.Domain.Shared.Users;

    #endregion

    /// <summary>
    ///     The application principal factory.
    /// </summary>
    public class ApplicationPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationPrincipalFactory"/> class.
        ///     The application principal factory constructor.
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="roleManager">
        /// The role manager.
        /// </param>
        /// <param name="optionsAccessor">
        /// The option accessor.
        /// </param>
        public ApplicationPrincipalFactory(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        /// <summary>
        /// Create user asynchronously
        /// </summary>
        /// <param name="user">
        /// The user to create.
        /// </param>
        /// <returns>
        /// The claims principal.
        /// </returns>
        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            ClaimsPrincipal principal = await base.CreateAsync(user);
            this.OnCreatePrincipal(principal, user);

            return principal;
        }

        /// <summary>
        /// Generate claims for user asynchronously.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The claims identity.
        /// </returns>
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            ClaimsIdentity identity = await base.GenerateClaimsAsync(user);
            this.OnGenerateClaims(identity, user);

            return identity;
        }

        private void OnCreatePrincipal(ClaimsPrincipal principal, User user)
        {
            throw new NotImplementedException();
        }

        private void OnGenerateClaims(ClaimsIdentity identity, User user)
        {
            throw new NotImplementedException();
        }
    }
}