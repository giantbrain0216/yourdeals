
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NPComplet.YourDeals.Domain.Shared.Constant.Permission;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Server.Api.Blazor.Controllers.Admin.Services.Identity.Interfaces;
using System;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Admin
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly UserManager<User> _userManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleService"></param>
        /// <param name="userManager"></param>
        public RoleController(IRoleService roleService, UserManager<User> userManager)
        {
            _roleService = roleService;
            _userManager = userManager;
        }

        /// <summary>
        /// Get All Roles (basic, admin etc.)
        /// </summary>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Roles.View)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        /// <summary>
        /// Add a Role
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Roles.Create)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Role request)
        {
            var response = await _roleService.SaveAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Delete a Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status 200 OK</returns>
        [Authorize(Policy = Permissions.Roles.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _roleService.DeleteAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Get Permissions By Role Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>Status 200 Ok</returns>
       [Authorize(Policy = Permissions.RoleClaims.View)]
        [HttpGet("{roleId}")]
       
        public async Task<IActionResult> GetPermissionsByRoleId(string roleId)
        {
            var response = await _roleService.GetAllPermissionsAsync(roleId);
            return Ok(response);
        }

        /// <summary>
        /// Edit a Role Claim
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Policy = Permissions.RoleClaims.Edit)]
        [HttpPut()]
        public async Task<IActionResult> Update([FromBody]PermissionViewModel model)
        {
            var user = _userManager.GetUserId(HttpContext.User);
            var response = await _roleService.UpdatePermissionsAsync(model,Guid.Parse(user));
            return Ok(response);
        }
    }
}