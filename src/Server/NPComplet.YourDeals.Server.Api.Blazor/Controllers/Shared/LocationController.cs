// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocationController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Shared
{
    #region

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using NPComplet.YourDeals.Domain.Shared.Shared;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;

    #endregion

    /// <summary>
    ///     the locations controller
    /// </summary>
    [ApiVersion("1.0")]
    public class LocationsController : TypeBaseController<Location>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationsController"/> class.
        ///     Ctor
        /// </summary>
        /// <param name="userManager">
        /// </param>
        /// <param name="unitOfWork">
        /// </param>
        /// <param name="logger">
        /// </param>
        public LocationsController(
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            ILogger<TypeBaseController<Location>> logger)
            : base(userManager, unitOfWork, logger)
        {
        }
    }
}