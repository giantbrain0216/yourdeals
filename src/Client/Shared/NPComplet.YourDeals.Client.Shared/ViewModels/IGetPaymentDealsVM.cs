// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGetPaymentDealsVM.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Client.Shared.ViewModels
{
    #region

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;

    #endregion

    /// <summary>
    ///     The GetPaymentDealsVM interface.
    /// </summary>
    public interface IGetPaymentDealsVM
    {
        /// <summary>
        ///     The get all payment deals.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task<List<CrossingPayment>> GetAllPaymentDeals();
    }
}