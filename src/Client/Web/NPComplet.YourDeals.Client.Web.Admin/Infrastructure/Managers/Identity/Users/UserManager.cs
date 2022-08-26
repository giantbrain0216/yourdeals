
using NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Extensions;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Managers.Identity.Users
{
    public class UserManager : IUserManager
    {
        private readonly HttpClient _httpClient;

        public UserManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<List<User>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.UserEndpoints.GetAll);
            return await response.ToResult<List<User>>();
        }

        public async Task<IResult<User>> GetAsync(string userId)
        {
            var response = await _httpClient.GetAsync(Routes.UserEndpoints.Get(userId));
            return await response.ToResult<User>();
        }

        public async Task<IResult> RegisterUserAsync(RegisterViewModel ViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoints.Register, ViewModel);
            return await response.ToResult<string>();
        }

        public async Task<IResult> ToggleUserStatusAsync(ToggleUserStatusViewModel ViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoints.ToggleUserStatus, ViewModel);
            return await response.ToResult();
        }

        public async Task<IResult<UserRolesViewModel>> GetRolesAsync(string userId)
        {
            var response = await _httpClient.GetAsync(Routes.UserEndpoints.GetUserRoles(userId));
            return await response.ToResult<UserRolesViewModel>();
        }

        public async Task<IResult> UpdateRolesAsync(UpdateUserRolesViewModel ViewModel)
        {
            var response = await _httpClient.PutAsJsonAsync(Routes.UserEndpoints.UpdateRoles, ViewModel);
            return await response.ToResult<UserRolesViewModel>();
        }

        public async Task<IResult> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoints.ForgotPassword, model);
            return await response.ToResult();
        }

        public async Task<IResult> ResetPasswordAsync(ResetPasswordViewModel ViewModel)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.UserEndpoints.ResetPassword, ViewModel);
            return await response.ToResult();
        }

        public async Task<string> ExportToExcelAsync(string searchString = "")
        {
            var response = await _httpClient.GetAsync(string.IsNullOrWhiteSpace(searchString)
                ? Routes.UserEndpoints.Export
                : Routes.UserEndpoints.ExportFiltered(searchString));
            var data = await response.Content.ReadAsStringAsync();
            return data;
        }
    }
}