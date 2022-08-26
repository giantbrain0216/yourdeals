// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FinnaceSupport.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Finance
{
    #region

    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;
    using NPComplet.YourDeals.Domain.Shared.Enumerable;

    #endregion

    /// <summary>
    /// </summary>
    [Table("FINANCESUPPORTS", Schema = "FINANCE")]
    public class FinanceSupport : BaseEntityDomain
    {
        /// <summary>
        /// 
        /// </summary>
        public string AliasName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FinanceSupportName FinanceSupportName { get; set; }

        /// <summary>
        ///     Define if is primary payment support for the user
        /// </summary>
        public bool IsPrimary { get; set; }
    }
}