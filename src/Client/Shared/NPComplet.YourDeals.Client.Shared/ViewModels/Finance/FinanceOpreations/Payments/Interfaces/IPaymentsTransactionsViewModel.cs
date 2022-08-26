using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Finance.FinanceOpreations.Payments.Interfaces
{
   public interface IPaymentsTransactionsViewModel
    {
        IList<CrossingPayment> CrossPayments { get; set; }
        Task GetTransactionsAsync(int pageindex, int size);
        Task<int> GetTransactionsCountAsync();
    }
}
