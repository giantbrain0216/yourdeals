// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropositionsController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Deals.BaseController
{
    #region

    using Infrastructure.Repositories.Interfaces;
    using Infrastructure.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.Exceptions;
    using NPComplet.YourDeals.Domain.Shared.Deal;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// the proposition controller
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class PropositionsController<T> : TypeBaseController<T>
        where T : Proposition
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IFinanceService FinanceService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="financeService"></param>
        protected PropositionsController(
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            ILogger<TypeBaseController<T>> logger, IFinanceService financeService)
            : base(userManager, unitOfWork, logger)
        {
            FinanceService = financeService;
        }

        /// <summary>
        /// Get propositions.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="includes">entities to include</param>
        /// <returns>Selected propositions.</returns>
        protected async Task<ActionResult<IEnumerable<T>>> GetPropositions(Expression<Func<T, bool>> filter, params string[] includes)
        {
            return Ok(await UnitOfWork.GetRepository<T>().GetAllAsync(
                filter,
                null,
                includes));
        }

        /// <summary>
        /// Update offer or request proposition.
        /// </summary>
        /// <param name="value">The proposition offer/request</param>
        /// <exception cref="ApplicationCoreException"></exception>
        protected virtual async Task<IActionResult> UpdatePropositionAsync(T value)
        {
            if (value == null)
            {
                throw new ApplicationCoreException(ErrorMesage.couldNotFindEntity);
            }

            var userId = UserManager.GetUserId(HttpContext.User);

            if (value.OwnerId != null && value.OwnerId.Value.ToString() == userId)
            {
                throw new ApplicationCoreException(ErrorMesage.userDoesNotHavePermissionToUpdateEntity);
            }

            UnitOfWork.GetRepository<T>().Update(value);
            await UnitOfWork.CommitAsync(userId);

            return Ok();
        }
    }
}