#region

using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Cybersource.Specifications;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces
{
    public interface ICyberSorurceFiananceGateWay
    {
        Task<CybersourcePaymentResponse> SimplePaymentWithCredidtCard(CybersourcePaymentRequest financeOperation);

        Task<CybersourcePayoutResponse> SimplePayoutWithNotTokenCard(CybersourcePayoutRequest financeOperation);

        Task<CybersourcePaymentResponse> SimplePaymentWithBankAccount(CybersourcePaymentRequest financeOperation);
    }
}