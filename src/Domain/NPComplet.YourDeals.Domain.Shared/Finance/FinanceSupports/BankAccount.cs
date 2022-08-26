namespace NPComplet.YourDeals.Domain.Shared.Finance.FinanceSupports
{
    using NPComplet.YourDeals.Domain.Shared.Enumerable;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    /// <summary>
    /// 
    /// </summary>
    [Table("BANKACCOUNTS", Schema = "FINANCE")]
    public class BankAccount : FinanceSupport
    {
        /// <summary>
        ///The finance support name.
        /// </summary>
        [DefaultValue(FinanceSupportName.BANKACCOUNT)]
        public new FinanceSupportName FinanceSupportName { get; set; }

        /// <summary>
        /// The BIC(Bank Identifier Code) identifying the account's bank. Optional, SlimPay can always compute a BIC when an IBAN is provided. Constraints: Text, 11 characters or less, Read-Write.
        /// </summary>
        public string BIC { get; set; }

        /// <summary>
        /// The IBAN(International Bank Account Number) identifying the bank account.Constraints: Text, 34 characters or less, Read-Write.
        /// </summary>
        public string IBAN { get; set; }

        /// <summary>
        /// The institution name of the bank.Constraints: Text, 32 characters or less, Read-Only.
        /// </summary>
        public string InstitutionName { get; set; }

        /// <summary>
        /// The account's country code. Optional, but if set, then sortCode and accountNumber must also be provided, while iban and bic must not. Constraints: Text, 2 characters, Read-Write.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// The account's sort code. Optional, but if set, then countryCode and accountNumber must also be provided, while iban and bic must not. Constraints: 6 or 8 characters, Read-Write.
        /// </summary>
        public string SortCode { get; set; }

        /// <summary>
        /// The account number. Optional, but if set, then sortCode and countryCode must also be provided, while iban and bic must not.Constraints: 8 characters, Read-Write.
        /// </summary>
        public string AccountNumber { get; set; }

    }
}
