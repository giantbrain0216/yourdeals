// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MarketsController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Markets
{
    #region

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using NPComplet.YourDeals.Domain.Shared.Markets;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;

    #endregion

    /// <summary>
    ///     the Market controller
    /// </summary>
    [ApiVersion("1.0")]
    public class MarketsController : TypeBaseController<Market>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarketsController"/> class.
        ///     List of market controller
        /// </summary>
        /// <param name="userManager">
        /// </param>
        /// <param name="unitOfWork">
        /// </param>
        /// <param name="logger">
        /// </param>
        public MarketsController(
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            ILogger<MarketsController> logger)
            : base(userManager, unitOfWork, logger)
        {
        }
    }
}