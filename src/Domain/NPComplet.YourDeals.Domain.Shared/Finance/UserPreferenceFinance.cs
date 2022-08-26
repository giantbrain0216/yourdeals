using System;
using System.Collections.Generic;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.Finance
{
    using NPComplet.YourDeals.Domain.Shared.Base;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 
    /// </summary>
    [Table("USERPREFERENCEFINANCE", Schema = "FINANCE")]
    public class UserPreferenceFinance:BaseEntityDomain
    {
        /// <summary>
        /// 
        /// </summary>
        public ICollection<FinanceSupport> AllFinanceSupports { get; set; }

    }
}
