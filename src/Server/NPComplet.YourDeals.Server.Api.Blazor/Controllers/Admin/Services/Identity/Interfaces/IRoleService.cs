using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Admin.Services.Identity.Interfaces
{
    public interface IRoleService 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<Role>>> GetAllAsync();
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
        Task<Result<Role>> GetByIdAsync(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Result<string>> SaveAsync(Role request);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result<string>> DeleteAsync(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<Result<PermissionViewModel>> GetAllPermissionsAsync(string roleId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Result<string>> UpdatePermissionsAsync(PermissionViewModel request, Guid usserId);
    }
}