using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Extensions;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Managers.Identity.Roles
{
    public class RoleManager : IRoleManager
    {
        private readonly HttpClient _httpClient;

        public RoleManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<string>> DeleteAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.RolesEndpoints.Delete}/{id}");
            return await response.ToResult<string>();
        }

        public async Task<IResult<List<Role>>> GetRolesAsync()
        {
            var response = await _httpClient.GetAsync(Routes.RolesEndpoints.GetAll);
            return await response.ToResult<List<Role>>();
        }

        public async Task<IResult<string>> SaveAsync(Role role)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.RolesEndpoints.Save, role);
            return await response.ToResult<string>();
        }

        public async Task<IResult<PermissionViewModel>> GetPermissionsAsync(string roleId)
        {
            var response = await _httpClient.GetAsync($"{Routes.RolesEndpoints.GetPermissions}/{roleId}");
            return await response.ToResult<PermissionViewModel>();
        }

        public async Task<IResult<string>> UpdatePermissionsAsync(PermissionViewModel request)
        {
            var response = await _httpClient.PutAsJsonAsync(Routes.RolesEndpoints.UpdatePermissions, request);
            return await response.ToResult<string>();
        }
    }
}