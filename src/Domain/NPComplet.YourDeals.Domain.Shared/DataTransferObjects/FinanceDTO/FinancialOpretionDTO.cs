using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.DataTransferObjects.FinanceDTO
{
    /// <summary>
    /// 
    /// </summary>
   public class FinancialOpretionDTO
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid FinanceOpreationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Enumerable.FinanceOpreationTable FinanceOpreationTable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid SelectedFinanceSupport { get; set; }

       
        /// <summary>
        /// 
        /// </summary>
        public CrossingPayment CrossingPayment { get; set; }
    }
}
