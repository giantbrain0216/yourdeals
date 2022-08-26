using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Admin.Services.Identity.Interfaces
{
    public interface IUserService 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<Result<List<User>>> GetAllAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IResult<User>> GetAsync(string userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        Task<IResult> RegisterAsync(RegisterViewModel request, string origin);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IResult> ToggleUserStatusAsync(ToggleUserStatusViewModel request);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult<UserRolesViewModel>> GetRolesAsync(string id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IResult> UpdateRolesAsync(UpdateUserRolesViewModel request,string userId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<IResult<string>> ConfirmEmailAsync(string userId, string code);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        Task<IResult> ForgotPasswordAsync(ForgotPasswordViewModel request, string origin);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IResult> ResetPasswordAsync(ResetPasswordViewModel request);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        Task<string> ExportToExcelAsync(string searchString = "");
    }
}