// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitBaseInMemeoryDatabase.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Tests.IntegrationTests
{
    #region

    using System;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Data;
    using NPComplet.YourDeals.Tests.TestsHelper;

    using Xunit.Abstractions;

    #endregion

    /// <summary>
    /// The unit base in memeory database.
    /// </summary>
    public class UnitBaseInMemeoryDatabase
    {
        protected DbContextOptions<ApplicationDbContext> Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitBaseInMemeoryDatabase"/> class.
        /// </summary>
        /// <param name="testOutputHelper">
        /// The test output helper.
        /// </param>
        public UnitBaseInMemeoryDatabase(ITestOutputHelper testOutputHelper)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var myLoggerFactory = new LoggerFactory(new[] { new XUnitLoggerProvider(new BaseTest(testOutputHelper)) });
            builder.UseLoggerFactory(myLoggerFactory);
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            this.Options = builder.Options;
        }
    }
}