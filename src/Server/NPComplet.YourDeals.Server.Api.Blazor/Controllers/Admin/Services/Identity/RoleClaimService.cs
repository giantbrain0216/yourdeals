using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using NPComplet.YourDeals.Domain.Shared.Base;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using NPComplet.YourDeals.Server.Api.Blazor.Controllers.Admin.Services.Identity.Interfaces;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
using NPComplet.YourDeals.Translations.Resources;

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Admin.Services.Identity
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleClaimService : IRoleClaimService
    {
        private readonly IStringLocalizer<Translation> _localizer;


       /// <summary>
       /// 
       /// </summary>
        protected readonly IUnitOfWork UnitOfWork;

      
       /// <summary>
       /// 
       /// </summary>
       /// <param name="localizer"></param>
       /// <param name="_Logger"></param>
       /// <param name="_UnitOfWork"></param>
        public RoleClaimService(
            IStringLocalizer<Translation> localizer,
           
            IUnitOfWork _UnitOfWork
           )
        {
            _localizer = localizer;
           
            UnitOfWork = _UnitOfWork;
            
        }

        public async Task<Result<List<RoleClaimViewModel>>> GetAllAsync()
        {
            var roleClaims = await UnitOfWork.GetRepository<BaseDomain>().GetAllClaimsAsync();
            
            return await Result<List<RoleClaimViewModel>>.SuccessAsync(roleClaims.ToList());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetCountAsync()
        {
            var roleClaims = await UnitOfWork.GetRepository<BaseDomain>().GetAllClaimsAsync();
            var count = roleClaims.Count();
            return count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<RoleClaimViewModel>> GetByIdAsync(int id)
        {
            var roleClaim = await UnitOfWork.GetRepository<BaseDomain>().GetRoleClaimById(id);
              
           
            return await Result<RoleClaimViewModel>.SuccessAsync(roleClaim);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<Result<List<RoleClaimViewModel>>> GetAllByRoleIdAsync(string roleId)
        {
            var roleClaims = await UnitOfWork.GetRepository<BaseDomain>().GetAllByRoleIdAsync(roleId);

            return await Result<List<RoleClaimViewModel>>.SuccessAsync(roleClaims.ToList());
        }
         /// <summary>
         /// 
         /// </summary>
         /// <param name="request"></param>
         /// <param name="userId"></param>
         /// <returns></returns>
        public async Task<Result<string>> SaveAsync(RoleClaimViewModel request,string userId)
        {
            if (string.IsNullOrWhiteSpace(request.RoleId))
            {
                return await Result<string>.FailAsync(_localizer["Role is required."]);
            }
            var message = await UnitOfWork.GetRepository<BaseDomain>().SaveClaimAsync(request, userId);
            return await Result<string>.SuccessAsync(message);
        }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <param name="userId"></param>
      /// <returns></returns>
        public async Task<Result<string>> DeleteAsync(int id,string userId)
        {
            var result = await UnitOfWork.GetRepository<BaseDomain>().DeleteRoleClaimAsync(id, userId);
             
           
                return await Result<string>.SuccessAsync(_localizer[result]);
           
        }
    }
}