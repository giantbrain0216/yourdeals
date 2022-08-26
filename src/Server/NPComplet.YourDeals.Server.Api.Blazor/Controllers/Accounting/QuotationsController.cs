// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QuotationsController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Accounting
{
    #region

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using NPComplet.YourDeals.Domain.Shared.Accounting;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using NPComplet.YourDeals.Domain.Shared.Wrapper;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    ///     the quotations controller
    /// </summary>
    [ApiVersion("1.0")]
    public class QuotationsController : TypeBaseController<Quotation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QuotationsController"/> class.
        ///     the quotation controller
        /// </summary>
        /// <param name="userManager">
        /// </param>
        /// <param name="unitOfWork">
        /// </param>
        /// <param name="logger">
        /// </param>
        public QuotationsController(
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            ILogger<QuotationsController> logger)
            : base(userManager, unitOfWork, logger)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<IEnumerable<Domain.Shared.Deal.Requests.PropositionRequest>>> GetQuotationByUserId([FromQuery] int offset, [FromQuery] int size)
        {

            var userId = UserManager.GetUserId(HttpContext.User);
            Guid.TryParse(userId, out var guid);
            var quotations =
                await UnitOfWork.GetRepository<Domain.Shared.Deal.Requests.PropositionRequest>().GetAllAsync(offset,
                size,
                u => u.Quotation.OwnerId == guid && !u.Quotation.IsAccepted && !u.Quotation.IsRejected,null,new string[] { "Quotation", "Quotation.Amounts", "Document","Owner"});

            if (quotations != null)
                return  await Result<IEnumerable<Domain.Shared.Deal.Requests.PropositionRequest>>.SuccessAsync(quotations);
            else
                return await Result<IEnumerable<Domain.Shared.Deal.Requests.PropositionRequest>>.FailAsync("Invoice error");





        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="quotation"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> QuotationAccpetReject([FromBody] Domain.Shared.Accounting.Quotation quotation)
        {

         
            return await Put(quotation.Id, quotation);


        }
    }
}