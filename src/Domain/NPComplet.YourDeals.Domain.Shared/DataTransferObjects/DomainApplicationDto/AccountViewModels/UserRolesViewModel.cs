using System.Collections.Generic;

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRolesViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<UserRoleModel> UserRoles { get; set; } = new();
    }
    /// <summary>
    /// 
    /// </summary>
    public class UserRoleModel
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool Selected { get; set; }
    }
}
