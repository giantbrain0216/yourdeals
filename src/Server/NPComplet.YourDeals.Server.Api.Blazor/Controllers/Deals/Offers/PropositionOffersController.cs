// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropositionOffersController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Deals.Offers
{
    #region

    using BaseController;
    using Infrastructure.Repositories.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
    using NPComplet.YourDeals.Domain.Shared.Finance;
    using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    #endregion

    /// <summary>
    ///     The proposition offer controller.
    /// </summary>
    [ApiVersion("1.0")]
    public class PropositionOffersController : PropositionsController<PropositionOffer>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="financeService"></param>

        public PropositionOffersController(
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            ILogger<PropositionOffersController> logger, IFinanceService financeService)
            : base(userManager, unitOfWork, logger, financeService)
        {
        }

        /// <summary>
        /// Get selected offers propositions.
        /// </summary>
        /// <param name="offerId">The offer identifier.</param>
        /// <returns>All offer propositions.</returns>
        [HttpGet("{offerId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PropositionOffer>>> GetSelectedPropositions(Guid offerId)
        {
            return await GetPropositions(propositionOffer => propositionOffer.OfferId == offerId && propositionOffer.IsSelected == true,
                "Invoice", "Invoice.Amounts", "PropositionMessagesPost");
        }

        /// <summary>
        /// Get offers propositions by offer identifier and owner.
        /// </summary>
        /// <param name="offerId">The offer identifier.</param>
        /// <param name="ownerId">The owner or proposal identifier.</param>
        /// <returns>All offer propositions.</returns>
        [HttpGet("{offerId:guid}/{ownerId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PropositionOffer>>> GetPropositions(Guid offerId, Guid ownerId)
        {
            return await GetPropositions(
                propositionOffer => propositionOffer.OfferId == offerId && propositionOffer.OwnerId == ownerId,
                "PropositionMessagesPost", "Invoice", "Invoice.Amounts");
        }

        /// <summary>
        /// Get offers propositions by offer identifier and owner.
        /// </summary>
        /// <param name="offerId">The offer identifier.</param>
        /// <returns>All offer propositions.</returns>
        [HttpGet("{offerId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PropositionOffer>>> GetPropositions(Guid offerId)
        {
            return await GetPropositions(
                propositionOffer => propositionOffer.OfferId == offerId,
                "PropositionMessagesPost", "Invoice", "Invoice.Amounts");
        }

        /// <summary>
        /// Create proposition offer.
        /// </summary>
        /// <param name="propositionOffer">The proposition offer.</param>
        /// <returns>Success</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePropositionOffer([FromBody] PropositionOffer propositionOffer)
        {
           
            propositionOffer.PropositionsFinanceOperation = new Domain.Shared.Finance.PropositionsFinanceOperation();
            var user = await UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                propositionOffer.PropositionsFinanceOperation.OwnerId = user?.Id;
                propositionOffer.PropositionsFinanceOperation.Owner = user;
            }
            return await Post(propositionOffer);
        }

        /// <summary>
        /// Select proposition of offer.
        /// </summary>
        /// <param name="id">The proposition identifier.</param>
        /// <returns>Success if the proposition updated correctly.</returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> SelectPropositionOffer(Guid id)
        {
            var propositionOffer = await UnitOfWork.GetRepository<PropositionOffer>().SingleOrDefaultAsync(p => p.Id == id, "Owner", "Invoice", "Invoice.Amounts");

            var paymentDeals = await FinanceService.InitQuotationPayment(propositionOffer);

            var defaultServiceGateway = await UnitOfWork.GetRepository<ServiceGateway>().SingleOrDefaultAsync(p => p.IsApplicationDefault == true);
            if (defaultServiceGateway != null)
            {
                paymentDeals.ForEach(crossingPayment =>
                {
                    crossingPayment.ServiceGatewayId = defaultServiceGateway.Id;
                    crossingPayment.DealId = propositionOffer.OfferId;
                    crossingPayment.DealType = Domain.Shared.Enumerable.DealType.Offer;
                    crossingPayment.PropositionId = propositionOffer.Id;
                });
            }

            await UnitOfWork.GetRepository<CrossingPayment>().AddRangeAsync(paymentDeals);
            await UnitOfWork.CommitAsync();

            propositionOffer.IsSelected = true;
            propositionOffer.SelectedOfferId = propositionOffer.OfferId;

            return await UpdatePropositionAsync(propositionOffer);
        }
    }
}