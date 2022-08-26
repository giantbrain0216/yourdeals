using System.Collections.Generic;

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels
{
/// <summary>
/// 
/// </summary>
    public class UpdateUserRolesViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IList<UserRoleModel> UserRoles { get; set; }
    }
}
