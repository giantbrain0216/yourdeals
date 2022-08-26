// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealsController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Deals.BaseController
{
    #region

    using Infrastructure.Repositories.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NPComplet.YourDeals.Domain.Shared.Deal;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    #endregion

    /// <summary>
    /// Abstract controller for both Offer and Request controllers
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public abstract class DealsController<T> : TypeBaseController<T>
        where T : Deal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DealsController{T}"/> class.
        ///     Ctor
        /// </summary>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        /// <param name="logger">
        /// The logger.
        /// </param>
        protected DealsController(
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            ILogger<TypeBaseController<T>> logger)
            : base(userManager, unitOfWork, logger)
        {
        }

        /// <summary>
        ///     Get all delas.
        /// </summary>
        /// <returns>All deals.</returns>
        [HttpGet]
        [AllowAnonymous]
        public virtual async Task<ActionResult<IEnumerable<T>>> GetAllDeals()
        {
            var deals = await this.UnitOfWork.GetRepository<T>().GetAllAsync(
                            null,
                            null,
                            "Address",
                            "Address.Location",
                            "DealDocument",
                            "DealDocument.DealFiles",
                            "SearchTags");

            return this.Ok(new List<T>(deals));
        }

        /// <summary>
        /// Get Deals by location
        /// </summary>
        /// <param name="latitude">
        /// X Location
        /// </param>
        /// <param name="longitude">
        /// Y location
        /// </param>
        /// <param name="zoom">
        /// Zoom location
        /// </param>
        /// <param name="orderBy">
        /// Order by parameter
        /// </param>
        /// <param name="include">
        /// include property
        /// </param>
        /// <returns>
        /// list of deals
        /// </returns>
        [HttpGet("{latitude}/{longitude}/{zoom}/{orderBy}/{*include}")]
        [AllowAnonymous]
        public virtual async Task<ActionResult<IEnumerable<T>>> GetDealsByLocation(
            double latitude,
            double longitude,
            double zoom,
            string orderBy,
            string include)
        {
            var includes = include.Split(" ");
            var deals = await this.UnitOfWork.GetRepository<T>().GetAllAsync(
                            d => (d.Address.Location.Latitude - latitude) * (d.Address.Location.Latitude - latitude)
                                 + (d.Address.Location.Longitude - longitude)
                                 * (d.Address.Location.Longitude - longitude) < zoom && d.InternalValidation,
                            orderBy,
                            includes);

            return this.Ok(deals);
        }

        /// <summary>
        /// send a post request.
        /// </summary>
        /// <param name="value">
        /// The value to post.
        /// </param>
        /// <returns>
        /// The post result.
        /// </returns>
        [HttpPost]
        public new async Task<IActionResult> Post([FromBody] T value)
        {
            
            value.DealFinanceOpreation = new Domain.Shared.Finance.DealsFinanceOpertation();
            var user = await UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                value.DealFinanceOpreation.OwnerId = user?.Id;
                value.DealFinanceOpreation.Owner = user;
            }
            return await base.Post(value);
        }

        /// <summary>
        /// Get deals with pagination.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="size">The size.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>Offer or request deals.</returns>
        protected async Task<ActionResult<IEnumerable<T>>> GetDealsWithPaginationAsync(int offset, int size, Expression<Func<T, bool>> filter)
        {
            return Ok(await UnitOfWork.GetRepository<T>().GetAllAsync(
                offset,
                size,
                filter,
                "InternalModificationDateTimeUtc",
                "DealDocument", "DealDocument.DealFiles", "DealPriceReference", "DealPriceReference.PaymentManors", "SelectedPropositions"));
        }
    }
}