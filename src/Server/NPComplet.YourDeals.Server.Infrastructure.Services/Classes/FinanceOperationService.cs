#region

using System;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Enumerable;
using NPComplet.YourDeals.Domain.Shared.Finance;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.CashIn;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
using NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Cybersource.Specifications;
using NPComplet.YourDeals.Domain.Shared.Pricing;
using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Classes
{
    public class FinanceOperationService : IFinanceOperationService
    {
        public Task<CybersourcePaymentRequest> PrepareDealPayment(CrossingPayment crossingPayment)
        {
            var cybersourcePaymentRequest = new CybersourcePaymentRequest();

            cybersourcePaymentRequest.FiniancialOpreationId = crossingPayment.Id;
            if (crossingPayment.FinanceSupportName == FinanceSupportName.CREDITCARD)
                cybersourcePaymentRequest.CreditCardId = crossingPayment.FinanceSupportId;
            else if (crossingPayment.FinanceSupportName == FinanceSupportName.BANKACCOUNT)
                cybersourcePaymentRequest.BankAccountId = crossingPayment.FinanceSupportId;
            cybersourcePaymentRequest.OwnerId = crossingPayment.OwnerId;

            return Task.FromResult(cybersourcePaymentRequest);
        }

        public Task<CybersourcePayoutRequest> PrepareDealPayout(CrossingPayout crossingPayout)
        {
            var cybersourcePayoutRequest = new CybersourcePayoutRequest();

            cybersourcePayoutRequest.FiniancialOpreationId = crossingPayout.Id;
            if (crossingPayout.FinanceSupportName == FinanceSupportName.CREDITCARD)
                cybersourcePayoutRequest.CreditCardId = crossingPayout.FinanceSupportId;
            else if (crossingPayout.FinanceSupportName == FinanceSupportName.BANKACCOUNT)
                cybersourcePayoutRequest.BankAccountId = crossingPayout.FinanceSupportId;
            cybersourcePayoutRequest.OwnerId = crossingPayout.OwnerId;

            return Task.FromResult(cybersourcePayoutRequest);
        }

        public Task<CybersourcePaymentRequest> PreparePostDealPayment(PostingDealPayment postDealPayment)
        {
            var cybersourcePaymentRequest = new CybersourcePaymentRequest();

            cybersourcePaymentRequest.FiniancialOpreationId = postDealPayment.Id;
            if (postDealPayment.FinanceSupportName == FinanceSupportName.CREDITCARD)
                cybersourcePaymentRequest.CreditCardId = postDealPayment.FinanceSupportId;
            else if (postDealPayment.FinanceSupportName == FinanceSupportName.BANKACCOUNT)
                cybersourcePaymentRequest.BankAccountId = postDealPayment.FinanceSupportId;
            cybersourcePaymentRequest.OwnerId = postDealPayment.OwnerId;

            return Task.FromResult(cybersourcePaymentRequest);
        }

        public Task<CybersourcePayoutRequest> PreapreWalletAmountPayout(FinanceSupport financeSupport,
            Amount WalletAmountayout, Guid User)
        {
            throw new NotImplementedException();
        }
    }
}