#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
using NPComplet.YourDeals.Domain.Shared.Deal.Requests;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceSettings.PostDeal;
using NPComplet.YourDeals.Domain.Shared.Pricing;
using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Classes
{
    public class FinanceService : IFinanceService
    {
        /// <summary>
        ///     Request Payments
        /// </summary>
        /// <param name="propositionRequest"></param>
        /// <returns></returns>
        public Task<List<CrossingPayment>> InitInvoicePayment(PropositionRequest propositionRequest)
        {
            var allpayment = new List<CrossingPayment>();
            foreach (var requiredamount in propositionRequest.Quotation.Amounts)
            {
                var crossingPayment = new CrossingPayment();
                crossingPayment.OwnerId = propositionRequest.Request.OwnerId;
                crossingPayment.Amount = new Amount { AmountValue = requiredamount.AmountValue };
                allpayment.Add(crossingPayment);
            }

            return Task.FromResult(allpayment);
        }

        /// <summary>
        ///     PostingDeal Payment
        /// </summary>
        /// <param name="postDealFeesTier"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<PostingDealPayment> InitPostingDealPayment(PostDealFeesTier postDealFeesTier, Guid userId)
        {
            var postingDealPayment = new PostingDealPayment
            {
                OwnerId = userId,
                Amount = new Amount { AmountValue = postDealFeesTier.Amount.AmountValue }
            };

            return Task.FromResult(postingDealPayment);
        }

        /// <summary>
        ///     offer Payment
        /// </summary>
        /// <param name="propositionOffer"></param>
        /// <returns></returns>
        public Task<List<CrossingPayment>> InitQuotationPayment(PropositionOffer propositionOffer)
        {
            var allPayment = new List<CrossingPayment>();
            foreach (var requiredAmount in propositionOffer.Invoice.Amounts)
            {
                var crossingPayment = new CrossingPayment
                {
                    OwnerId = propositionOffer.OwnerId,
                    Amount = new Amount
                    {
                        AmountValue = requiredAmount.AmountValue
                    }
                };

                allPayment.Add(crossingPayment);
            }

            return Task.FromResult(allPayment);
        }
    }
}