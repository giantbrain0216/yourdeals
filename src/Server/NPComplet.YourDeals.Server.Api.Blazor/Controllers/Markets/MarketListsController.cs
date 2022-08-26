// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MarketListsController.cs" company="NPComplet">
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
    ///     the List Markets  controller
    /// </summary>
    [ApiVersion("1.0")]
    public class MarketListsController : TypeBaseController<Markets>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarketListsController"/> class.
        ///     Markets controllers
        /// </summary>
        /// <param name="userManager">
        /// </param>
        /// <param name="unitOfWork">
        /// </param>
        /// <param name="logger">
        /// </param>
        public MarketListsController(
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            ILogger<MarketListsController> logger)
            : base(userManager, unitOfWork, logger)
        {
        }
    }
}