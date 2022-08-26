// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainsEnumerable.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Domain.Shared.Enumerable
{
    #region

    using System.Runtime.Serialization;

    #endregion
    /// <summary>
    /// 
    /// </summary>
    public enum AuditType : byte
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }
    /// <summary>
    /// 
    /// </summary>
    public enum EntityExtendedAttributeType : byte
    {
        /// <summary>
        /// 
        /// </summary>
        Decimal = 1,
        /// <summary>
        /// 
        /// </summary>
        Text = 2,
        /// <summary>
        /// 
        /// </summary>
        DateTime = 3,
        /// <summary>
        /// 
        /// </summary>
        Json = 4
    }
    /// <summary>
    /// 
    /// </summary>
    public enum CashFlow
    {
        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "CashIn")]
        CashIn,
        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "CashOut")]
        CashOut
    }
    /// <summary>
    /// 
    /// </summary>
    public enum FinanceOpreationTable
    {
        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "CrossingPayment")]
        CrossingPayment,
        /// <summary>
        /// 
        /// </summary>
        [EnumMember(Value = "DealPostingPayment")]
        DealPostingPayment
    }
    /// <summary>
    /// 
    /// </summary>
    public enum DealType
    {
        /// <summary>
        /// </summary>
        [EnumMember(Value = "Offer")]
        Offer,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "Request")]
        Request,
    }
    /// <summary>
    ///     the credit card type
    /// </summary>
    public enum CreditCardType
    {
        /// <summary>
        /// </summary>
        [EnumMember(Value = "VISA")]
        Visa,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "MASTERCARD")]
        Mastercard,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "DISCOVER")]
        Discover,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "AMEX")]
        Amex,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "OTHER")]
        Other
    }

    /// <summary>
    ///     DealPriceManor
    /// </summary>
    public enum PaymentManor
    {
        /// <summary>
        ///     block the money on this System to be Approved also Default for Milestone
        /// </summary>
        [EnumMember(Value = "Pay After")]
        PayAfter,

        /// <summary>
        ///     Pay diretly to the owner
        /// </summary>
        [EnumMember(Value = "Pay Before")]
        PayBefore,

        /// <summary>
        /// The milestone payment.
        /// </summary>
        [EnumMember(Value = "Milestone payment")]
        DelayedPayment
    }

    /// <summary>
    ///     the address type
    /// </summary>
    public enum AddressType
    {
        /// <summary>
        /// </summary>
        [EnumMember(Value = "HOME")]
        Home,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "WORK")]
        Work,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "OTHER")]
        Other
    }


    /// <summary>
    ///  By default Ordinary
    /// </summary>
    public enum FinanceOperationType
    {
        /// <summary>
        /// </summary>
        [EnumMember(Value = "Before")]
        Before,


        /// <summary>
        /// </summary>
        [EnumMember(Value = "After")]
        After,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "Many")]
        Many,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "Other")]
        Other
    }


    /// <summary>
    /// </summary>
    public enum FinanceOperationState
    {
        /// <summary>
        /// </summary>
        [EnumMember(Value = "PENDING")]
        Pending,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "SUCCESS")]
        Success,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "REFUSED")]
        Refused
    }

    /// <summary>
    /// </summary>
    public enum ServiceGatewayName
    {
        /// <summary>
        /// </summary>
        [EnumMember(Value = "CYBERSOURCE")]
        CYBERSOURCE,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "STRIPE")]
        STRIPE,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "OTHERS")]
        OTHERS
    }


    /// <summary>
    /// </summary>
    public enum FinanceSupportName
    {
        /// <summary>
        /// </summary>
        [EnumMember(Value = "CREDITCARD")]
        CREDITCARD,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "BANKACCOUNT")]
        BANKACCOUNT,


        /// <summary>
        /// </summary>
        [EnumMember(Value = "PAYPAL")]
        PAYPAL,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "OTHERS")]
        OTHERS
    }

    




    public enum FinanceGateWayOperationState
    {
        /// <summary>
        /// </summary>
        [EnumMember(Value = "PENDING")]
        Pending,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "SUCCESS")]
        Success,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "REFUSED")]
        Refused
    }


    public enum CallState
    {
        /// <summary>
        /// </summary>
        [EnumMember(Value = "OK")]
        OK,

        /// <summary>
        /// </summary>
        [EnumMember(Value = "KO")]
        KO
    }

    /// <summary>
    ///  define thge unit source type by default is Ordinary
    /// </summary>
    public enum UnitSourceType
    {
        /// <summary>
        /// Ordinary unit
        /// </summary>
        [EnumMember(Value = "Ordinary")]
        Ordinary,

        /// <summary>
        /// HourlyFlatRateofLabor
        /// </summary>
        [EnumMember(Value = "HourlyFlatRateofLabor")]
        HourlyFlatRateofLabor,

        /// <summary>
        /// TravellingExpenses
        /// </summary>
        [EnumMember(Value = "TravellingExpenses")]
        TravellingExpenses,

        /// <summary>
        /// Other
        /// </summary>
        [EnumMember(Value = "Other")]
        Other
    }
}