namespace NPComplet.YourDeals.Domain.Shared.Finance
{
    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;
    /// <summary>
    /// 
    /// </summary>

    [Table("SERVICEGATEWAY", Schema = "FINANCE")]
    public class ServiceGateway : BaseEntityDomain
    {

        /// <summary>
        ///  the service gateway name (eg ; Stripe , Paypal ....)
        /// </summary>
        public string ServiceGatewayName { get; set; }


        /// <summary>
        ///  this this default for the application 
        /// </summary>

        public bool IsApplicationDefault { get; set; }
    }
}
