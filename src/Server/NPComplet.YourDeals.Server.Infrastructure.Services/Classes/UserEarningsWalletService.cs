#region

using System;
using System.Linq;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.Finance.EarningsWallet;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Classes
{
    public class UserEarningsWalletService : IUserEarningsWalletService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserEarningsWalletService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task AddWalletTransaction(UserEarningsWallet userEarningsWallet)
        {
            throw new NotImplementedException();
        }

        public async Task<UserEarningsWallet> GetUserBalance(Guid userId)
        {
            var userbalance =
                await _unitOfWork.GetRepository<UserEarningsWallet>().GetAllAsync(p => p.OwnerId == userId);

            if (userbalance.Any())
            {
                var lastuserbalancestate = userbalance.OrderByDescending(p => p.CreationDateTimeUtc).FirstOrDefault();
                if (lastuserbalancestate != null)
                    return lastuserbalancestate;
                return null;
            }

            return null;
        }
    }
}