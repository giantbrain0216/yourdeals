using NPComplet.YourDeals.Domain.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;


namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Managers.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManager
    {
        Task<IResult<List<RoleClaimViewModel>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimViewModel>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimViewModel role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}