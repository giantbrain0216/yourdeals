using System;
using System.Collections.Generic;
using System.Text;

namespace NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Stripe
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("STRIPESERVICEGATEWAY", Schema = "FINANCE")]
    public class StripeServiceGateway : ServiceGateway
    {
    }
}
