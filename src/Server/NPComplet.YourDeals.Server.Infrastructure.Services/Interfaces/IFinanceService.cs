#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
using NPComplet.YourDeals.Domain.Shared.Deal.Requests;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceSettings.PostDeal;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces
{
    public interface IFinanceService
    {
        // Setup Operation 
        Task<List<CrossingPayment>> InitQuotationPayment(PropositionOffer propositionOffer);
        Task<PostingDealPayment> InitPostingDealPayment(PostDealFeesTier postDealFeesTier, Guid userId);

        Task<List<CrossingPayment>> InitInvoicePayment(PropositionRequest propositionRequest);
    }
}