// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericRepositoryUnitTests.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Tests.Infrastructure.Tests
{
    #region

    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using NPComplet.YourDeals.Domain.Shared.Shared;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Classes;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Data;
    using NPComplet.YourDeals.Tests.TestsHelper;

    using Xunit;
    using Xunit.Abstractions;

    #endregion

    /// <summary>
    /// The generic repository unit tests.
    /// </summary>
    public class GenericRepositoryUnitTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepositoryUnitTests"/> class.
        /// </summary>
        /// <param name="testOutputHelper">
        /// The test output helper.
        /// </param>
        public GenericRepositoryUnitTests(ITestOutputHelper testOutputHelper)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true).Build();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.UseSqlServer(configuration["ConnectionString:SQLConnectionString"]);
            var myLoggerFactory = new LoggerFactory(new[] { new XUnitLoggerProvider(new BaseTest(testOutputHelper)) });
            builder.UseLoggerFactory(myLoggerFactory);
            this._options = builder.Options;

            // .UseInMemoryDatabase(Guid.NewGuid().ToString())
            // .Options;
        }

        /// <summary>
        /// The get all async_ tests.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task GetAllAsync_Tests()
        {
            using (var context = new ApplicationDbContext(this._options))
            {
                var repository = new GenericRepository<Post>(context);

                // for (int i = 0; i < 100000; i++)
                // {
                // await repository.AddAsync(new Post { Test = i });
                // }
                await context.CommitAsync();

                // var result = repository.GetAllAsync(x => x.CreationDateTimeUtc % 2 == 0).Result.Count();

                // result.Should().Be(100000);
            }
        }
    }
}