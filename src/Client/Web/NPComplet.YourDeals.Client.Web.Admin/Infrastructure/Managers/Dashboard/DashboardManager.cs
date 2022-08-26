
using NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Extensions;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using System.Net.Http;
using System.Threading.Tasks;


namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Managers.Dashboard
{
    public class DashboardManager : IDashboardManager
    {
        private readonly HttpClient _httpClient;

        public DashboardManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<DashboardDataResponse>> GetDataAsync()
        {
            var response = await _httpClient.GetAsync(Routes.DashboardEndpoints.GetData);
            var data = await response.ToResult<DashboardDataResponse>();
            return data;
        }
    }
}