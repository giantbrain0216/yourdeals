
using NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Managers;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Managers.Identity.Roles
{
    public interface IRoleManager : IManager
    {
        Task<IResult<List<Role>>> GetRolesAsync();

        Task<IResult<string>> SaveAsync(Role role);

        Task<IResult<string>> DeleteAsync(string id);

        Task<IResult<PermissionViewModel>> GetPermissionsAsync(string roleId);

        Task<IResult<string>> UpdatePermissionsAsync(PermissionViewModel request);
    }
}