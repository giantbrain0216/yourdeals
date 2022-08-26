namespace NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Stripe.Specifications
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;

    [Table("STRIPPAYMENTS", Schema = "FINANCE")]
    public class StipePayment : BaseEntityDomain
    {
        public Guid? FinanceOperationId { get; set; }

        /// <summary>
        ///  can be DealPayment , or Posting DelPayment
        /// </summary>
        public FinanceOperation FinanceOperation { get; set; }
    }
}
