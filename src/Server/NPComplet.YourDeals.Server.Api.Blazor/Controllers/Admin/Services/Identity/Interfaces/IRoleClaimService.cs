using NPComplet.YourDeals.Domain.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;


namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Admin.Services.Identity.Interfaces
{
    /// <summary>
    /// /
    /// </summary>
    public interface IRoleClaimService 
    {
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        Task<Result<List<RoleClaimViewModel>>> GetAllAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<RoleClaimViewModel>> GetByIdAsync(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<Result<List<RoleClaimViewModel>>> GetAllByRoleIdAsync(string roleId);
 /// <summary>
 /// 
 /// </summary>
 /// <param name="request"></param>
 /// <param name="userId"></param>
 /// <returns></returns>
        Task<Result<string>> SaveAsync(RoleClaimViewModel request, string userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<Result<string>> DeleteAsync(int id, string userId);
    }
}