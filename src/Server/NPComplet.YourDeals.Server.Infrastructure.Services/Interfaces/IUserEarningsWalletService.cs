#region

using System;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Finance.EarningsWallet;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces
{
    public interface IUserEarningsWalletService
    {
        Task<UserEarningsWallet> GetUserBalance(Guid userId);

        Task AddWalletTransaction(UserEarningsWallet userEarningsWallet);
    }
}