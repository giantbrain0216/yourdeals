// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeSignInManager.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Tests.TestsHelper
{
    #region

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Moq;

    using NPComplet.YourDeals.Domain.Shared.Users;

    #endregion

    /// <summary>
    /// The fake sign in manager.
    /// </summary>
    public class FakeSignInManager : SignInManager<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeSignInManager"/> class.
        /// </summary>
        public FakeSignInManager()
            : base(
                new Mock<FakeUserManager>().Object,
                new HttpContextAccessor(),
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<User>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<User>>().Object)
        {
        }
    }
}