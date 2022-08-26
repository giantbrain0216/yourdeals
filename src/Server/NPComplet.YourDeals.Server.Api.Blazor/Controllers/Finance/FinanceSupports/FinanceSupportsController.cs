using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPComplet.YourDeals.Domain.Shared.Finance;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.Exceptions;

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Finance.FinanceSupports
{

    /// <summary>
    /// 
    /// </summary>
    public class FinanceSupportsController<T> : TypeBaseController<FinanceSupport> where T : FinanceSupport
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IFinanceService _financeService;
        /// <summary>
        /// for tokenize it in future
        /// </summary>
        protected readonly ICyberSorurceFiananceGateWay _cyberSorurceFiananceGateWay;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="financeService"></param>
        /// <param name="cyberSorurceFiananceGateWay"></param>
        public FinanceSupportsController(UserManager<User> userManager, IUnitOfWork unitOfWork, ILogger<FinanceSupportsController<T>> logger, IFinanceService financeService, ICyberSorurceFiananceGateWay cyberSorurceFiananceGateWay)
            : base(userManager, unitOfWork, logger)
        {
            _financeService = financeService;
            _cyberSorurceFiananceGateWay = cyberSorurceFiananceGateWay;
        }
        /// <summary>
        ///     Get all delas.
        /// </summary>
        /// <returns>All deals.</returns>
        [HttpGet]

        public virtual async Task<ActionResult<IEnumerable<T>>> GetAllUserFinanceSupports()
        {
            var userId = UserManager.GetUserId(HttpContext.User);
            var financesupports = await this.UnitOfWork.GetRepository<T>().GetAllAsync(
                            (p => p.OwnerId == Guid.Parse(userId) && p.InternalIsDelete != true),
                            null
                           );

            return this.Ok(await Result<List<T>>.SuccessAsync(financesupports.ToList()));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public new async Task<IActionResult> Put(Guid Id, [FromBody] T value)
        {
            return await base.Put(Id, value);
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
            var userId = UserManager.GetUserId(HttpContext.User);
            value.OwnerId = Guid.Parse(userId);
            return await base.Post(value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public new async Task<IActionResult> Delete(Guid Id )
        {
            return await base.Delete(Id);
        }
}
}
