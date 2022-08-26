using Microsoft.AspNetCore.Identity;
using NPComplet.YourDeals.Domain.Shared.Constant.Permission;
using NPComplet.YourDeals.Domain.Shared.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels;

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Admin.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class ClaimsHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="allPermissions"></param>
        public static void GetAllPermissions(this List<RoleClaimViewModel> allPermissions)
        {
            var modules = typeof(Permissions).GetNestedTypes();

            foreach (var module in modules)
            {
                var fields = module.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

                foreach (FieldInfo fi in fields)
                {
                    var propertyValue = fi.GetValue(null);

                    if (propertyValue is not null)
                        allPermissions.Add(new RoleClaimViewModel { Value = propertyValue.ToString(), Type = ApplicationClaimTypes.Permission, Group = module.Name });
                    //TODO - take descriptions from description attribute
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="role"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        public static async Task<IdentityResult> AddPermissionClaim(this RoleManager<Role> roleManager, Role role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            if (!allClaims.Any(a => a.Type == ApplicationClaimTypes.Permission && a.Value == permission))
            {
                return await roleManager.AddClaimAsync(role, new Claim(ApplicationClaimTypes.Permission, permission));
            }

            return IdentityResult.Failed();
        }
    }
}
