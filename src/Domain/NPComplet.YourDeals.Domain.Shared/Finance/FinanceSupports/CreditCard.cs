// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreditCard.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Finance.FinanceSupports
{

    #region

    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Enumerable;
    using NPComplet.YourDeals.Domain.Shared.Shared;

    #endregion

    /// <summary>
    ///     the credit card
    /// </summary>
    [Table("CREDITCARDS", Schema = "FINANCE")]
    public class CreditCard : FinanceSupport
    {
        /// <summary>
        /// The finance support name.
        /// </summary>
        [DefaultValue(FinanceSupportName.CREDITCARD)]
        public new FinanceSupportName FinanceSupportName { get; set; }
        /// <summary>
        ///     the billing address
        /// </summary>
        public Address BillingAddress { get; set; }

        /// <summary>
        ///     the billing address Id
        /// </summary>
        public Guid? BillingAddressId { get; set; }

        /// <summary>
        ///     the country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CardHolderName { get; set; }
        /// <summary>
        ///     the creation date time
        /// </summary>
        public DateTime CreateTimeUtc { get; set; }

        /// <summary>
        ///     the CVV2 number
        /// </summary>
        public string Cvv2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ExpirationDate { get; set; }
        /// <summary>
        ///     the expiration month
        /// </summary>
        public int ExpireMonth { get; set; }

        /// <summary>
        ///     the expiration year
        /// </summary>
        public int ExpireYear { get; set; }

        /// <summary>
        ///     te first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     the last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     the number of the credit card
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        ///     the State
        /// </summary>
        public string State { get; set; }

        /// <summary>
        ///     the credit card type
        /// </summary>
        public CreditCardType Type { get; set; }

        /// <summary>
        ///     the update date time
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        ///     the valid until datetime
        /// </summary>
        public DateTime ValidUntilUtc { get; set; }
    }
}