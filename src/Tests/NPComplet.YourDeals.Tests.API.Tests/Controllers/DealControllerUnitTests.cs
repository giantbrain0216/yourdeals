// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealControllerUnitTests.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Tests.API.Tests.Controllers
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using FluentAssertions;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using Moq;

    using NPComplet.YourDeals.Domain.Shared.Deal;
    using NPComplet.YourDeals.Domain.Shared.Deal.Requests;
    using NPComplet.YourDeals.Domain.Shared.Shared;
    using NPComplet.YourDeals.Server.Api.Blazor.Controllers.Deals;
    using NPComplet.YourDeals.Server.Api.Blazor.Controllers.Deals.Requests;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
    using NPComplet.YourDeals.Tests.TestsHelper;

    using Xunit;
    using Xunit.Abstractions;

    #endregion

    /// <summary>
    /// The deal controller unit tests.
    /// </summary>
   /* public class DealControllerUnitTests : UnitBase
    {
        private readonly Mock<ILogger<RequestsController>> _logger = new();

        private readonly Mock<IUnitOfWork> _unitOfWork = new();

        private readonly Mock<FakeUserManager> _userManager = new();

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
        /// The get deals by location_ should retrun_ correct number.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [Fact]
        public async Task GetDealsByLocation_ShouldRetrun_CorrectNumber()
        {
            var repo = new Mock<IGenericRepository<Request>>();
            repo.Setup(
                    x => x.GetAllAsync(
                        It.IsAny<Expression<Func<Request, bool>>>(),
                        It.IsAny<string>(),
                        It.IsAny<string[]>()))
                .ReturnsAsync(
                    new List<Request>
                        {
                            new() { Address = new Address { Location = new Location { Latitude = 1, Longitude = 0 } } },
                            new()
                                {
                                    Address = new Address
                                                  {
                                                      Location = new Location { Latitude = 500, Longitude = 200 }
                                                  }
                                }
                        });

            this._unitOfWork.Setup(x => x.GetRepository<Request>()).Returns(repo.Object);

            var controller = new RequestsController(
                this._userManager.Object,
                this._unitOfWork.Object,
                this._logger.Object);

            var result = await controller.GetDealsByLocation(500,500, 1000, null, "Address");

            // Assert
            var okObjectResult = result.Result as OkObjectResult;
            var data = okObjectResult.Value as IEnumerable<Deal>;

            data.Count().Should().Be(2);
        }
    }*/
}