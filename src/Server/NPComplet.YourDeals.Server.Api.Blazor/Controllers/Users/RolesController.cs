// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RolesController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Users
{
    #region

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using NPComplet.YourDeals.Domain.Shared.Users;

    #endregion

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class RolesController : ControllerBase
    {
        /// <summary>
        ///     The logger.
        /// </summary>
        private readonly ILogger<RolesController> _logger;

        /// <summary>
        ///     The role manager.
        /// </summary>
        private readonly RoleManager<Role> _roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController"/> class.
        /// </summary>
        /// <param name="roleManager">
        /// The role manager.
        /// </param>
        /// <param name="logger">
        /// The logger. 
        /// </param>
        public RolesController(RoleManager<Role> roleManager, ILogger<RolesController> logger)
        {
            this._roleManager = roleManager;
            this._logger = logger;
        }

        /// <summary>
        /// Create role
        /// </summary>
        /// <param name="model">
        /// The role to create
        /// </param>
        /// <returns>
        /// Created role.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateRole(Role model)
        {
            try
            {
                if (model != null)
                    await this._roleManager.CreateAsync(new Role { Name = model.Name, Description = model.Name });
                return this.Ok(model);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        ///     Get roles.
        /// </summary>
        /// <returns>Roles.</returns>
        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult GetAllRoles()
        {
            try
            {
                return this.Ok(this._roleManager.Roles.ToList());
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                throw;
            }
        }
    }
}