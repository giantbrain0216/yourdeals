// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DealCashIn.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.CashIn
{
    #region

    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    /// <summary>
    ///     The deal cash in class.
    /// </summary>
    [Table("CROSSINGPAYOUTS", Schema = "FINANCE")]
    public class CrossingPayout : FinanceOperation
    {

    }
}