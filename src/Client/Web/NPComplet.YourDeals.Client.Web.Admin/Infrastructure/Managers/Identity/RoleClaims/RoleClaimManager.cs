using NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Extensions;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;


namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Managers.Identity.RoleClaims
{
    public class RoleClaimManager : IRoleClaimManager
    {
        private readonly HttpClient _httpClient;

        public RoleClaimManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.RoleClaimsEndpoints.Delete}/{id}");
            return await response.ToResult<string>();
        }

        public async Task<IResult<List<RoleClaimViewModel>>> GetRoleClaimsAsync()
        {
            var response = await _httpClient.GetAsync(Routes.RoleClaimsEndpoints.GetAll);
            return await response.ToResult<List<RoleClaimViewModel>>();
        }

        public async Task<IResult<List<RoleClaimViewModel>>> GetRoleClaimsByRoleIdAsync(string roleId)
        {
            var response = await _httpClient.GetAsync($"{Routes.RoleClaimsEndpoints.GetAll}/{roleId}");
            return await response.ToResult<List<RoleClaimViewModel>>();
        }

        public async Task<IResult<string>> SaveAsync(RoleClaimViewModel role)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.RoleClaimsEndpoints.Save, role);
            return await response.ToResult<string>();
        }
    }
}