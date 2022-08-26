// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropositionRequestsController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Deals.Requests
{
    #region

    using BaseController;
    using Infrastructure.Repositories.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NPComplet.YourDeals.Domain.Shared.Deal.Requests;
    using NPComplet.YourDeals.Domain.Shared.Finance;
    using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    #endregion

    /// <summary>
    ///     The proposition request controller.
    /// </summary>
    [ApiVersion("1.0")]
    public class PropositionRequestsController : PropositionsController<PropositionRequest>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="financeService"></param>
        public PropositionRequestsController(
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            ILogger<PropositionRequestsController> logger, IFinanceService financeService)
            : base(userManager, unitOfWork, logger, financeService)
        {
        }

        /// <summary>
        /// Get selected requests propositions.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <returns>All request propositions.</returns>
        [HttpGet("{requestId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PropositionRequest>>> GetSelectedPropositions(Guid requestId)
        {
            return await GetPropositions(propositionRequest => propositionRequest.RequestId == requestId && propositionRequest.IsSelected == true, 
                "Quotation", "Quotation.Amounts", "PropositionMessagesPost");
        }

        /// <summary>
        /// Get requests propositions by request identifier.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <param name="ownerId">The owner identifier.</param>
        /// <returns>All request propositions.</returns>
        [HttpGet("{requestId:guid}/{ownerId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PropositionRequest>>> GetPropositions(Guid requestId, Guid ownerId)
        {
            return await GetPropositions(
                propositionRequest => propositionRequest.RequestId == requestId && propositionRequest.OwnerId == ownerId,
                "Document", "Document.DealFiles", "PropositionMessagesPost", "Quotation", "Quotation.Amounts");
        }

        /// <summary>
        /// Get requests propositions by request identifier and owner.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <returns>All request propositions.</returns>
        [HttpGet("{requestId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PropositionRequest>>> GetPropositions(Guid requestId)
        {
            return await GetPropositions(
                propositionRequest => propositionRequest.RequestId == requestId,
                "Document", "Document.DealFiles", "PropositionMessagesPost", "Quotation",
                "Quotation.Amounts", "Quotation.TermConditionPost", "Quotation.WarrantyPost");
        }

        /// <summary>
        /// Create proposition request.
        /// </summary>
        /// <param name="propositionRequest">The proposition request.</param>
        /// <returns>Success</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePropositionRequest([FromBody] PropositionRequest propositionRequest)
        {
            try
            {
                
                propositionRequest.PropositionsFinanceOperation = new Domain.Shared.Finance.PropositionsFinanceOperation();
                var user = await UserManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    propositionRequest.PropositionsFinanceOperation.OwnerId = user?.Id;
                    propositionRequest.PropositionsFinanceOperation.Owner = user;
                }
                return await Post(propositionRequest);
            }
            catch (Exception ex)
            {

                throw;
                
            }
           
        }

        /// <summary>
        /// Select proposition of request.
        /// </summary>
        /// <param name="id">The proposition identifier.</param>
        /// <returns>Success if proposition updated correctly.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> SelectPropositionRequest(Guid id)
        {
            var propositionRequest = await UnitOfWork.GetRepository<PropositionRequest>().SingleOrDefaultAsync(p => p.Id == id, "Request", "Owner", "Quotation", "Quotation.Amounts");

            var paymentDeals = await FinanceService.InitInvoicePayment(propositionRequest);

            var defaultServiceGateway = await UnitOfWork.GetRepository<ServiceGateway>().SingleOrDefaultAsync(p => p.IsApplicationDefault == true);
            if (defaultServiceGateway != null)
            {
                paymentDeals.ForEach(delegate (CrossingPayment crossingPayment)
                {
                    crossingPayment.ServiceGatewayId = defaultServiceGateway.Id;
                    crossingPayment.DealId = propositionRequest.RequestId;
                    crossingPayment.DealType = Domain.Shared.Enumerable.DealType.Request;
                    crossingPayment.PropositionId = propositionRequest.Id;
                });
            }

            await UnitOfWork.GetRepository<CrossingPayment>().AddRangeAsync(paymentDeals);
            await UnitOfWork.CommitAsync();

            propositionRequest.IsSelected = true;
            propositionRequest.SelectedRequestId = propositionRequest.RequestId;

            return await UpdatePropositionAsync(propositionRequest);
        }
    }
}