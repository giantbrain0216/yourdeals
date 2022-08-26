#region

using System.Threading.Tasks;
using NPComplet.YourDeals.Server.Infrastructure.Services.ConfigurationSettings;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces
{
    public interface IFiananceGateWay
    {
        Task<CysberSorurceFiananceGateWaySetting> InitCyberSourceConfigruation();

        Task InitStripeConfiguration();
    }
}