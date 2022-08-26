using System;
using System.Collections.Generic;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Stripe.Specifications
{
    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;

    [Table("STRIPPAYOUTS", Schema = "FINANCE")]
    public class StripePayout : BaseEntityDomain
    {
        public Guid? FinanceOperationId { get; set; }
        /// <summary>
        ///  can be DealPayment , or Posting DelPayment
        /// </summary>
        public FinanceOperation FinanceOperation { get; set; }

    }
}
