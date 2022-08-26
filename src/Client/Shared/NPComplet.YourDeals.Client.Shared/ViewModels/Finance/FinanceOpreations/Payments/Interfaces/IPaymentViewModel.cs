using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.FinanceDTO;
using NPComplet.YourDeals.Domain.Shared.Finance;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceSupports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Shared.ViewModels.Finance.FinanceOpreations.Payments.Interfaces
{
   public interface IPaymentViewModel
    {
        CrossingPayment CrossingPayment { get; set; }

        PostingDealPayment PostingDealPayment { get; set; }
        FinancialOpretionDTO financialOpreationDto { get; set; }

        List<CreditCard> financeSupports { get; set; }
        Task GetFinancialSupportsAsync();
        Task GetCrossingPaymentAsync(Guid Id);
        Task GetPostingDealPaymentAsync(Guid Id);
        Task DeletePaymentMethod(Guid Id);
        Task PostPaymentAsync();
    }
}
