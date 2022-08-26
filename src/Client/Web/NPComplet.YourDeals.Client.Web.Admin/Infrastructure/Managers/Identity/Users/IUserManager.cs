using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Managers.Identity.Users
{
    public interface IUserManager : IManager
    {
        Task<IResult<List<User>>> GetAllAsync();

        Task<IResult> ForgotPasswordAsync(ForgotPasswordViewModel request);

        Task<IResult> ResetPasswordAsync(ResetPasswordViewModel request);

        Task<IResult<User>> GetAsync(string userId);

        Task<IResult<UserRolesViewModel>> GetRolesAsync(string userId);

        Task<IResult> RegisterUserAsync(RegisterViewModel request);

        Task<IResult> ToggleUserStatusAsync(ToggleUserStatusViewModel request);

        Task<IResult> UpdateRolesAsync(UpdateUserRolesViewModel request);

        Task<string> ExportToExcelAsync(string searchString = "");
    }
}