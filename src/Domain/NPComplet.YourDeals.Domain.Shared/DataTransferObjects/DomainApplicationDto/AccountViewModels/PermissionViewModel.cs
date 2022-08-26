using System.Collections.Generic;

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects.DomainApplicationDto.AccountViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RoleClaimViewModel> RoleClaims { get; set; }
    }
}
