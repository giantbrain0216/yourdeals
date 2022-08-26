#region

using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.CashIn;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
using NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Cybersource.Specifications;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces
{
    public interface IFinanceOperationService
    {
        Task<CybersourcePaymentRequest> PreparePostDealPayment(PostingDealPayment postDealPayment);

        Task<CybersourcePaymentRequest> PrepareDealPayment(CrossingPayment crossingPayment);

        Task<CybersourcePayoutRequest> PrepareDealPayout(CrossingPayout crossingPayout);

        // Task<CybersourcePayoutRequest> PreapreWalletAmountPayout(FinanceSupport financeSupport, Amount WalletAmountayout, Guid User);
    }
}