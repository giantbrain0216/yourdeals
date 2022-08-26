namespace NPComplet.YourDeals.Domain.Shared.Finance.FinanceSupports
{
    using NPComplet.YourDeals.Domain.Shared.Enumerable;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PAYPALACCOUNT", Schema = "FINANCE")]
    public class PayPalAccount : FinanceSupport
    {
        /// <summary>
        /// The finance support name.
        /// </summary>
        [DefaultValue(FinanceSupportName.PAYPAL)]
        public new FinanceSupportName FinanceSupportName { get; set; }

        /// <summary>
        /// PayPal Merchant ID
        /// </summary>
        public string PayPalMerchantID { get; set; }
    }
}
