using Microsoft.AspNetCore.Identity;
using NPComplet.YourDeals.Domain.Shared.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.Users
{
    /// <summary>
    /// 
    /// </summary>
   public class RoleClaim :IdentityRoleClaim<Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual Role Role { get; set; }
    }
}
