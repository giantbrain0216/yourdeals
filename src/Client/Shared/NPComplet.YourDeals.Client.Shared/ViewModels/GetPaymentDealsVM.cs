// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetPaymentDealsVM.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using NPComplet.YourDeals.Client.Shared.ViewModels.Base;

namespace NPComplet.YourDeals.Client.Shared.ViewModels
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using NPComplet.YourDeals.Client.Shared.Helpers;
    using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;

    #endregion

    /// <summary>
    ///     The get payment deals vm.
    /// </summary>
    public class GetPaymentDealsVM : BaseViewModel , IGetPaymentDealsVM
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPaymentDealsVM"/> class.
        /// </summary>
        /// <param name="httpClient">
        /// The http client.
        /// </param>
        public GetPaymentDealsVM(HttpClient httpClient)
        {
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.PaymentDealsList = new List<CrossingPayment>();
        }

        /// <summary>
        ///     Gets or sets the payment deals list.
        /// </summary>
        public List<CrossingPayment> PaymentDealsList { get; set; }

        /// <summary>
        ///     Get All Payment Deals
        /// </summary>
        /// <returns>Payment deals.</returns>
        public async Task<List<CrossingPayment>> GetAllPaymentDeals()
        {
            try
            {
                var result = await this._httpClient.GetFromJsonAsync<CrossingPayment[]>(Config.GetAllUserTransaction);

                if (result == null || !result.Any()) return this.PaymentDealsList;

                foreach (var deal in result) this.PaymentDealsList.Add(deal);

                return this.PaymentDealsList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}