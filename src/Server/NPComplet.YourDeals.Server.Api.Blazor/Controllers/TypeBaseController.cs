// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeBaseController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers
{
    #region

    using Infrastructure.Repositories.Interfaces;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NPComplet.YourDeals.Domain.Shared.Base;
    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.Exceptions;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public abstract class TypeBaseController<T> : ControllerBase
        where T : BaseEntityDomain
    {
        /// <summary>
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// </summary>
        protected readonly IUnitOfWork UnitOfWork;

         

        /// <summary>
        /// </summary>
        protected readonly UserManager<User> UserManager;

        /// <inheritdoc />
        protected TypeBaseController(
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            ILogger<TypeBaseController<T>> logger)
        {
            UserManager = userManager;
            UnitOfWork = unitOfWork;
            Logger = logger;
        }

        /// <summary>
        ///     Get count valid by  owned (logged user)
        ///     Authenticated user
        /// </summary>
        /// <returns>the count of valid entities</returns>
        [HttpGet]
        public virtual async Task<ActionResult<long>> Count()
        {
            var userId = UserManager.GetUserId(HttpContext.User);
            return Ok(await UnitOfWork.GetRepository<T>().CountAsync(d => d.OwnerId.ToString() == userId && d.InternalValidation));
        }
/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <returns></returns>
        [HttpDelete("{Id}")]
       protected virtual async Task<IActionResult> Delete(Guid Id)
        {
            var userId = UserManager.GetUserId(HttpContext.User);
            var entity = await UnitOfWork.GetRepository<T>().GetByIdAsync(Id);
            if (entity == null)
                throw new ApplicationCoreException(ErrorMesage.couldNotFindEntity);
            if (entity.OwnerId != null && entity.OwnerId.Value.ToString() != userId)
                throw new ApplicationCoreException(ErrorMesage.userDoesNotHavePermissionToUpdateEntity);
            entity.InternalIsDelete = true;
            await this.UnitOfWork.GetRepository<T>().UpdateAsync(entity);
            await this.UnitOfWork.CommitAsync(userId);
            return this.Ok();
        }
   

    /// <summary>
    /// Get valid entity  by  owned (logged user)
    ///     Authenticated user
    /// </summary>
    /// <param name="indexFrom">
    /// index from 
    /// </param>
    /// <param name="indexTo">
    /// index to the count 
    /// </param>
    /// <param name="orderBy">
    /// order by to the count , the name of the property separated with point like (Property) 
    /// </param>
    /// <param name="include">
    /// Include  properties  Sample ("Address Address.Location") 
    /// </param>
    /// <returns>
    /// valid entities 
    /// </returns>
    [HttpGet]
        protected async Task<ActionResult<IEnumerable<T>>> Get(
            int indexFrom,
            int indexTo,
            string orderBy,
            string include)
        {
            var userId = UserManager.GetUserId(HttpContext.User);
            var ds = await UnitOfWork.GetRepository<T>().GetAllAsync(
                         indexFrom,
                         indexTo,
                         d => d.OwnerId.ToString() == userId && d.InternalValidation,
                         orderBy,
                         include?.Split(" "));
            return Ok(ds);
        }

        /// <summary>
        /// Add entity  this will override the OwnerId
        ///     Authenticated user
        /// </summary>
        /// <param name="value">
        /// value 
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPost]
        protected virtual async Task<IActionResult> Post([FromBody] T value)
        {
            if (HttpContext.User != null)
            {
                var user = await UserManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    value.OwnerId = user?.Id;
                    value.Owner = user;
                }
                await this.UnitOfWork.GetRepository<T>().AddAsync(value);
                await this.UnitOfWork.CommitAsync(user?.Id.ToString());
            }
            else
            {
                await this.UnitOfWork.GetRepository<T>().AddAsync(value);
                await this.UnitOfWork.CommitAsync();
            }
         
            return this.Ok();
        }

        /// <summary>
        /// Update entity  only by te ownerID (logged user)
        ///     Authenticated user
        /// </summary>
        /// <param name="id">
        /// EntityID 
        /// </param>
        /// <param name="value">
        /// entity  
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        [HttpPut("{id}")]
        protected virtual async Task<IActionResult> Put(Guid id, [FromBody] T value)
        {
            var userId = UserManager.GetUserId(HttpContext.User);
            var entity = await UnitOfWork.GetRepository<T>().GetByIdAsync(value.Id);
            if (entity == null)
                throw new ApplicationCoreException(ErrorMesage.couldNotFindEntity);
            if (entity.OwnerId != null && entity.OwnerId.Value.ToString() != userId)
                throw new ApplicationCoreException(ErrorMesage.userDoesNotHavePermissionToUpdateEntity);
            await this.UnitOfWork.GetRepository<T>().UpdateAsync(value);
            await this.UnitOfWork.CommitAsync(userId);
            return this.Ok();
        }

        /// <summary>
        /// Get deals with pagination.
        /// </summary>
        /// <param name="offset">The start index.</param>
        /// <param name="size">The size.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>Offer or request deals.</returns>
       
    }
}