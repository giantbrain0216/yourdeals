using Microsoft.EntityFrameworkCore;
using NPComplet.YourDeals.Domain.Shared.Accounting;
using NPComplet.YourDeals.Domain.Shared.Communication;
using NPComplet.YourDeals.Domain.Shared.Deal;
using NPComplet.YourDeals.Domain.Shared.Deal.Offers;
using NPComplet.YourDeals.Domain.Shared.Deal.Requests;
using NPComplet.YourDeals.Domain.Shared.Document;
using NPComplet.YourDeals.Domain.Shared.Finance;
using NPComplet.YourDeals.Domain.Shared.Finance.EarningsWallet;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.CashIn;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceSettings;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceSettings.Deal;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceSettings.PostDeal;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceSupports;
using NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways;
using NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Cybersource;
using NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Cybersource.Specifications;
using NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Stripe;
using NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Stripe.Specifications;
using NPComplet.YourDeals.Domain.Shared.Markets;
using NPComplet.YourDeals.Domain.Shared.Monitoring;
using NPComplet.YourDeals.Domain.Shared.Pricing;
using NPComplet.YourDeals.Domain.Shared.Shared;
using NPComplet.YourDeals.Domain.Shared.Users;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Server.Infrastructure.Repositories.Data
{
    public class ApplicationDbContext : AuditableContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(p => p.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<User>().ToTable("AspNetUsers", "rightnde_sa");
            //builder.Entity<User>().ToTable("AspNetUserClaims", "rightnde_sa");
            //builder.Entity<User>().ToTable("AspNetUserRoles", "rightnde_sa");
            //builder.Entity<Role>().ToTable("AspNetRoles", "rightnde_sa");
            //builder.Entity<RoleClaim>().ToTable("AspNetRoleClaims", "rightnde_sa");
        }
        #region Finance
        public virtual DbSet<FinanceOperation>  FinanceOperations { get; set; }
        public virtual DbSet<DealsFinanceOpertation>  DealsFinanceOpertations { get; set; }

        public virtual DbSet<FinanceSupport> FinanceSupports { get; set; }
        public virtual DbSet<PropositionsFinanceOperation> PropositionsFinanceOpertations { get; set; }
        public virtual DbSet<ServiceGateway> ServiceGateways { get; set; }
        public virtual DbSet<UserPreferenceFinance> UserPreferenceFinances { get; set; }
        public virtual DbSet<CrossingPayment> DealPayments { get; set; }

        public virtual DbSet<PostingDealPayment>  PostingDealPayments { get; set; }
        public virtual DbSet<CrossingPayout> DealPayouts { get; set; }

        public virtual DbSet<NPCompletEarningsWallet> NPCompletEarningsWallets { get; set; }

        public virtual DbSet<UserEarningsWallet> UserEarningsWallets { get; set; }

        public virtual DbSet<FeesTire> FeesTires { get; set; }

        public virtual DbSet<DealFeesTier> DealFeesTiers { get; set; }

        public virtual DbSet<PostDealFeesTier> PostDealFeesTiers { get; set; }

        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<CreditCard> CreditCards { get; set; }

        public virtual DbSet<PayPalAccount> PayPalAccounts { get; set; }

        public virtual DbSet<GateWayOperationPayment> GateWayOperationPayments { get; set; }
        public virtual DbSet<GateWayOperationPayout>  GateWayOperationPayouts { get; set; }
        public virtual DbSet<GateWayOperationRequest> GateWayOperationRequests { get; set; }

        public virtual DbSet<GateWayOperationResponse> GateWayOperationResponses { get; set; }
        public virtual DbSet<CybersourceServiceGateway> CybersourceServiceGateways { get; set; }

        public virtual DbSet<CybersourcePayment> CybersourcePayments { get; set; }
        public virtual DbSet<CybersourcePaymentRequest> CybersourcePaymentRequests { get; set; }
        public virtual DbSet<CybersourcePaymentResponse> CybersourcePaymentResponses { get; set; }
        public virtual DbSet<CybersourcePayoutRequest>  CybersourcePayoutRequests { get; set; }
        public virtual DbSet<CybersourcePayout> CybersourcePayouts { get; set; }

        public virtual DbSet<CybersourcePayoutResponse> CybersourcePayoutResponses { get; set; }

        #endregion
        #region Messages

        /// <summary>
        ///     DbSet Messages
        /// </summary>
        public virtual DbSet<Message> Messages { get; set; }

        #endregion

        #region DealDocuments

        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<DealDocument> DealDocuments { get; set; }
        #endregion

        #region Users

        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<ProfileImage> ProfileImages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual DbSet<Profile> Profiles { get; set; }
        #endregion


        public virtual async Task<int> CommitAsync(string userid = null)
        {
            if (userid == null)
            {
                return await base.SaveChangesAsync();
            }
            else
            {
                return await base.SaveChangesAsync(userid);
            }

        }

        #region Deals

        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Request> Requests { get; set; }

        #endregion

        #region Documents

        /// <summary>
        ///     DbSet Documents
        /// </summary>
        public virtual DbSet<Post> Posts { get; set; }


        /// <summary>
        ///     DbSet CommentedPosts
        /// </summary>
        public virtual DbSet<DealMessagesPost> DealMessagesPosts { get; set; }

        /// <summary>
        ///     DbSet CommentedPosts
        /// </summary>
        public virtual DbSet<PropositionMessagesPost> PropositionMessagesPosts { get; set; }

        /// <summary>
        ///     DbSet Files
        /// </summary>
        public virtual DbSet<StoredFile> Files { get; set; }

        #endregion

        #region invoices

        /// <summary>
        ///     DbSet Invoices
        /// </summary>
        public virtual DbSet<Invoice> Invoices { get; set; }


        /// <summary>
        ///     Dbset Quotations
        /// </summary>
        public virtual DbSet<Quotation> Quotations { get; set; }

        #endregion

        #region Market

        /// <summary>
        ///     DbSet Markets
        /// </summary>
        public virtual DbSet<Market> Markets { get; set; }


        /// <summary>
        ///     DbSet All Markets
        /// </summary>
        public virtual DbSet<Markets> AllMarkets { get; set; }

        #endregion

        #region Payements

     

        /// <summary>
        ///     DbSet Currencies
        /// </summary>
        public virtual DbSet<Currency> Currencies { get; set; }


       


        /// <summary>
        ///     DbSet Units
        /// </summary>
        public virtual DbSet<UnitPricing> Units { get; set; }

        #endregion


        #region Shared

        /// <summary>
        ///     DbSet Addresses
        /// </summary>
        public virtual DbSet<Address> Addresses { get; set; }

        /// <summary>
        ///     DbSet Locations
        /// </summary>
        public virtual DbSet<Location> Locations { get; set; }


        /// <summary>
        ///     DbSet Companies
        /// </summary>
        public virtual DbSet<Company> Companies { get; set; }

        /// <summary>
        ///     DbSet Signatures
        /// </summary>
        public virtual DbSet<Signature> Signatures { get; set; }

        #endregion

        #region Monitoring 

        /// <summary>
        ///     DbSet Addresses
        /// </summary>
        public virtual DbSet<Log> Logs { get; set; }

        #endregion


    }
}