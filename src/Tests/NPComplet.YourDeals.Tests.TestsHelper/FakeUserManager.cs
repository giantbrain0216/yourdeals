// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FakeUserManager.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Tests.TestsHelper
{
    #region

    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using Moq;

    using NPComplet.YourDeals.Domain.Shared.Users;

    #endregion




    /// <summary>
    /// The fake user manager.
    /// </summary>
    public class FakeUserManager : UserManager<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeUserManager"/> class.
        /// </summary>
        public FakeUserManager()
            : base(
                new Mock<IUserStore<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<User>>().Object,
                Array.Empty<IUserValidator<User>>(),
                Array.Empty<IPasswordValidator<User>>(),
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<User>>>().Object)
        {
        }


    }


    public class FakeRoleManager :RoleManager<Role>
    {
        public FakeRoleManager()
            : base(new Mock <IRoleStore<Role>>().Object,
                Array.Empty < IRoleValidator <Role>>(), 
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<Role>>>().Object)
        {
        }
    }
}
