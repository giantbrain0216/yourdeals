// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealControllerUnitTests.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Tests.IntegrationTests.Controller
{
    #region

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Moq;

    using NPComplet.YourDeals.Domain.Shared.Deal;
    using NPComplet.YourDeals.Domain.Shared.Shared;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using NPComplet.YourDeals.Server.Api.Blazor.Controllers.Deals;
    using NPComplet.YourDeals.Server.Api.Blazor.Controllers.Deals.Requests;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Classes;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Data;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
    using NPComplet.YourDeals.Tests.TestsHelper;

    using Xunit;
    using Xunit.Abstractions;

    #endregion

    /// <summary>
    /// The deal controller unit tests.
    /// </summary>
    /*public class DealControllerUnitTests : UnitBaseInMemeoryDatabase
    {
        private readonly Mock<ILogger<RequestsController>> _logger = new();

        private readonly UserManager<User> _userManager = new FakeUserManager();

        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="DealControllerUnitTests"/> class.
        /// </summary>
        /// <param name="testOutputHelper">
        /// The test output helper.
        /// </param>
        public DealControllerUnitTests(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
        }

        /// <summary>
        /// The get_ should retrun_ set the correct filter.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task Get_ShouldRetrun_SetTheCorrectFilter()
        {
            using (var context = new ApplicationDbContext(this.Options))
            {
                this._unitOfWork = new UnitOfWork(context);

                await this._unitOfWork.GetRepository<Deal>().AddAsync(
                    new Deal
                        {
                            InternalValidation = true,
                            Address = new Address { Location = new Location { Latitude = 1, Longitude = 2 } }
                        });

                await this._unitOfWork.GetRepository<Deal>().AddAsync(
                    new Deal
                        {
                            InternalValidation = true,
                            Address = new Address { Location = new Location { Latitude = 50, Longitude = 50 } }
                        });

                await this._unitOfWork.CommitAsync();

                var controller = new RequestsController(this._userManager, this._unitOfWork, this._logger.Object);
                var s = "Address Address.Location";
                var result = await this._unitOfWork.GetRepository<Deal>().GetAllAsync(
                                 0,
                                 2,
                                 d => d.InternalValidation,
                                 "Y",
                                 s.Split(" "));

                result.Count().Should().Be(2);

                var result2 = await controller.GetDealsByLocation(1, 0, 10, null, "Address");

                // Assert
                var okObjectResult = result2.Result as OkObjectResult;
                var data = okObjectResult.Value as IEnumerable<Deal>;

                data.Count().Should().Be(1);
            }
        }

        /// <summary>
        /// The get deals by location_ should retrun_ correct number.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task GetDealsByLocation_ShouldRetrun_CorrectNumber()
        {
            using (var context = new ApplicationDbContext(this.Options))
            {
                this._unitOfWork = new UnitOfWork(context);

                await this._unitOfWork.GetRepository<Deal>().AddAsync(
                    new Deal
                        {
                            InternalValidation = true,
                            Address = new Address { Location = new Location { Latitude = 1, Longitude = 2 } }
                        });

                await this._unitOfWork.GetRepository<Deal>().AddAsync(
                    new Deal
                        {
                            InternalValidation = true,
                            Address = new Address { Location = new Location { Latitude = 8, Longitude = 10 } }
                        });

                await this._unitOfWork.CommitAsync();

                var controller = new RequestsController(this._userManager, this._unitOfWork, this._logger.Object);

                var result = await controller.GetDealsByLocation(1, 2, 8, null, "Address Address.Location");

                // Assert
                var okObjectResult = result.Result as OkObjectResult;
                var data = okObjectResult.Value as IEnumerable<Deal>;

                data.Count().Should().Be(1);
            }
        }
    }*/
}