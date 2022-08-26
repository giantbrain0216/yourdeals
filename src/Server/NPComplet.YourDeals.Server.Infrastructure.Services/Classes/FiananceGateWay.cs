#region

using System;
using System.Threading.Tasks;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
using NPComplet.YourDeals.Server.Infrastructure.Services.ConfigurationSettings;
using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Classes
{
    public class FiananceGateWay : IFiananceGateWay
    {
        private readonly IUnitOfWork _unitOfWork;

        public FiananceGateWay(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<CysberSorurceFiananceGateWaySetting> InitCyberSourceConfigruation()
        {
            throw new NotImplementedException();
        }

        public Task InitStripeConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}