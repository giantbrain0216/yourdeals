namespace NPComplet.YourDeals.Domain.Shared.Finance
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using NPComplet.YourDeals.Domain.Shared.Base;
    using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.CashIn;
    using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
    /// <summary>
    /// 
    /// </summary>
    [Table("USERWALLETS", Schema = "FINANCE")]
    public class UserWallet : BaseEntityDomain
    {
        /// <summary>
        ///  Add finance operations
        /// </summary>
        public ICollection<FinanceOperation> AllFinanceOperations { get; set; }

        /// <summary>
        /// all PostingDealPayment FinanceOperations
        /// </summary>
        public ICollection<PostingDealPayment> PostingDealPaymentFinanceOperations { get; set; }



        /// <summary>
        ///  ALl Deals Fianance Operation
        /// </summary>
        public DealsFinanceOpertation DealsFinanceOpertation { get; set; }


        /// <summary>
        ///  ALl Propositions Finance Opertation
        /// </summary>
        public PropositionsFinanceOperation PropositionsFinanceOperation { get; set; }

    }
}
