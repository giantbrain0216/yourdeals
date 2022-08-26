// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitBase.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Tests.API.Tests
{
    #region

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Data;
    using NPComplet.YourDeals.Tests.TestsHelper;

    using Xunit.Abstractions;

    #endregion

    /// <summary>
    /// The unit base.
    /// </summary>
    public class UnitBase
    {
        protected DbContextOptions<ApplicationDbContext> Options;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitBase"/> class.
        /// </summary>
        /// <param name="testOutputHelper">
        /// The test output helper.
        /// </param>
        public UnitBase(ITestOutputHelper testOutputHelper)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var myLoggerFactory = new LoggerFactory(new[] { new XUnitLoggerProvider(new BaseTest(testOutputHelper)) });
            builder.UseLoggerFactory(myLoggerFactory);
            this.Options = builder.Options;
        }
    }
}