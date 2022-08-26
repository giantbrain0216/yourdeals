// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Config.cs" company="NPComplet">
//   FR-SAS
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Acr.UserDialogs;

namespace NPComplet.YourDeals.Client.Shared.Helpers
{
    /// <summary>
    /// The config.
    /// </summary>
    public class Config
    {


        #region App
        /// <summary>
        /// The base url.
        /// </summary>
        public const string BaseUrl = ApiUrl;

        public const string BaseWebAppUrl = WebAppUrl;
        //private const string V = "https://innovicsoft.com";
        private const string V = "https://uat.api.rightndeals.com/";
        //private const string V = "https://localhost:5669/";

        /// <summary>
        /// The uat API url.
        /// </summary>
        private const string ApiUrl = V;
        

        
        private const string V2 = "https://uat.rightndeals.com/";
        private const string WebAppUrl = V2;



        /// <summary>
        ///     SignalR Constant
        /// </summary>
        /// <summary>
        /// The common api url.
        /// </summary>
        public const string CommonApiUrl = "api/v1/";
        #endregion
        #region Offers

        /// <summary>
        /// The get all offers deals
        /// </summary>
        public const string GetAllOffers = CommonApiUrl + "Offers/GetAllDeals";

        /// <summary>
        /// The get near offers.
        /// </summary>
        public const string GetNearOffers = CommonApiUrl + "Offers/GetDealsByLocation";

        /// <summary>
        /// The get offers with pagination
        /// </summary>
        public const string GetOffersWithPagination = CommonApiUrl + "Offers/GetOffers?offset={0}&size={1}";

        /// <summary>
        /// The get offers count.
        /// </summary>
        public const string GetOffersCount = CommonApiUrl + "Offers/Count";

        /// <summary>
        /// The get offer by identifier.
        /// </summary>
        public const string GetOfferById = CommonApiUrl + "Offers/GetDealById/";

        /// <summary>
        /// Create Offer.
        /// </summary>
        public const string CreateOffer = CommonApiUrl + "Offers/Post";

        /// <summary>
        /// The selected proposition offers.
        /// </summary>
        public const string SelectedPropositionOffers = CommonApiUrl + "PropositionOffers/GetSelectedPropositions/{0}";

        /// <summary>
        /// The proposition offers route.
        /// </summary>
        public const string PropositionOffers = CommonApiUrl + "PropositionOffers/GetPropositions/{0}";

        /// <summary>
        /// The proposition offers by owner route.
        /// </summary>
        public const string PropositionOffersByOwner = CommonApiUrl + "PropositionOffers/GetPropositions/{0}/{1}";

        /// <summary>
        /// The create proposition offer.
        /// </summary>
        public const string CreatePropositionOffer = CommonApiUrl + "PropositionOffers/CreatePropositionOffer";

        /// <summary>
        /// The select proposition offer route
        /// </summary>
        public const string SelectPropositionOffer = CommonApiUrl + "PropositionOffers/SelectPropositionOffer/{0}";

        #endregion

        #region Requests

        /// <summary>
        /// The get all deals.
        /// </summary>
        public const string GetAllRequests = CommonApiUrl + "Requests/GetAllDeals";

        /// <summary>
        /// Get request deal by identifier.
        /// </summary>
        public const string GetRequestById = CommonApiUrl + "Requests/GetDealById/";

        /// <summary>
        /// Create Offer.
        /// </summary>
        public const string CreateRequest = CommonApiUrl + "Requests/Post";

        /// <summary>
        /// The get requests count.
        /// </summary>
        public const string GetRequestsCount = CommonApiUrl + "Requests/Count";

        /// <summary>
        /// The get requests with pagination.
        /// </summary>
        public const string GetRequestsWithPagination = CommonApiUrl + "Requests/GetRequests?offset={0}&size={1}";

        /// <summary>
        /// The selected proposition requests
        /// </summary>
        public const string SelectedPropositionRequests = CommonApiUrl + "PropositionRequests/GetSelectedPropositions/{0}";

        /// <summary>
        /// The propositions requests route.
        /// </summary>
        public const string PropositionRequests = CommonApiUrl + "PropositionRequests/GetPropositions/{0}";

        /// <summary>
        /// The proposition requests by owner route.
        /// </summary>
        public const string PropositionRequestsByOwner = CommonApiUrl + "PropositionRequests/GetPropositions/{0}/{1}";

        /// <summary>
        /// The create proposition request.
        /// </summary>
        public const string CreatePropositionRequest = CommonApiUrl + "PropositionRequests/CreatePropositionRequest";

        /// <summary>
        /// The selected proposition request route.
        /// </summary>
        public const string SelectPropositionRequest = CommonApiUrl + "PropositionRequests/SelectPropositionRequest/{0}";

        #endregion

        #region Identity
        /// <summary>
        /// The google maps api key.
        /// </summary>
        public const string GoogleMapsApiKey = "AIzaSyDim5vSE_Qn1ojr8MIzMIsKiCjGvA6HkV8";

        /// <summary>
        /// The login.
        /// </summary>
        public const string Login = CommonApiUrl + "Accounts/Login";
        /// <summary>
        /// The ExternalLogin
        /// </summary>
        public const string ExternalLogin = CommonApiUrl + "Accounts/ExternalLogin";

        /// <summary>
        /// ValidateFacebookAccessToken
        /// </summary>
        public const string ValidateAccessToken = CommonApiUrl + "Accounts/ValidateAccessToken";

        public const string RegisterWithExternalProvider = CommonApiUrl + "Accounts/RegisterWithExternalProvider";

        public const string GetFacebookAppId = CommonApiUrl + "Accounts/GetFacebookAppId";

        public const string GetGoogleAppId = CommonApiUrl + "Accounts/GetGoogleAppId";


        /// <summary>
        /// 
        /// </summary>
        public const string ConfirmEmail = CommonApiUrl + "Accounts/ConfirmEmail";
        /// <summary>
        /// 
        ///   
        /// </summary>
        public const string ResetPassword = CommonApiUrl + "Accounts/ResetPassword";
        /// <summary>
        /// 
        /// </summary>
        public const string SendVerificationEmail = CommonApiUrl + "Accounts/SendVerificationEmailByEmailTwo";

        public const string ReSendVerificationEmail = CommonApiUrl + "Accounts/SendVerificationEmail";

        public const string ForgotPassword = CommonApiUrl + "Accounts/ForgotPassword";

        /// <summary>
        /// 
        /// </summary>
        public const string Register = CommonApiUrl + "Accounts/Register";
        public const string CurrentUser = CommonApiUrl + "Accounts/Login";
        public const string RefreshToken = CommonApiUrl + "Accounts/Refresh";
        /// <summary>
        /// Log Out
        /// </summary>
        public const string Logout = CommonApiUrl + "Accounts/Logout";

        #endregion

        public const string GetTags = CommonApiUrl + "Tagging/GetTags/";

        /// <summary>
        /// Get All Users
        /// </summary>
        public const string GetAllUsers = CommonApiUrl + "Users/GetAllUsers";

        /// <summary>
        /// Get User By Id
        /// </summary>
        public const string GetUserById = CommonApiUrl + "Users/GetUserById";

        public const string GetUserEmailById = CommonApiUrl + "Users/GetUserEmailById";


        /// <summary>
        /// Get All Roles
        /// </summary>
        public const string GetAllRoles = CommonApiUrl + "Roles/GetAllRoles";

        #region Finance
        /// <summary>
        /// Get All Payment Deals
        /// </summary>
        public const string GetAllUserTransaction = CommonApiUrl + "DealPayment/GetAllUserTransaction?offset={0}&size={1}";
        public const string GetDealPaymentsCount = CommonApiUrl + "DealPayment/Count";
        public const string GetTransactionById = CommonApiUrl + "DealPayment/GetTransactionById?Id={0}";
        public const string GetUserCreditCards = CommonApiUrl + "CreditCards/GetAllUserFinanceSupports";
        public const string SaveUserCreditCard = CommonApiUrl + "CreditCards/Post";
        public const string UpdateUserCreditCard = CommonApiUrl + "CreditCards/Put/{0}";
        public const string DeleteUserCreditCard = CommonApiUrl + "CreditCards/Delete/{0}";
        public const string DopaywithCredit = CommonApiUrl + "DealPayment/DoPaymentWithCreditCard";
        #endregion
        #region Message

        /// <summary>
        /// 
        /// </summary>
        public const string GetUserAllChatHistory = CommonApiUrl + "Messages/GetUserAllChatHistory";

        /// <summary>
        /// 
        /// </summary>
        public const string GetUserConvertion = CommonApiUrl + "Messages/GetChatHistory";

        /// <summary>
        /// The save message route.
        /// </summary>
        public const string SaveMessage = CommonApiUrl + "Messages/SaveMessage";

        /// <summary>
        /// Close messages route.
        /// </summary>
        public const string CloseMessages = CommonApiUrl + "Messages/CloseMessages";

        /// <summary>
        /// The messages of room route.
        /// </summary>
        public const string RoomMessages = CommonApiUrl + "Messages/GetRoomMessages/{0}";

        #endregion

        /// <summary>
        /// Create Offer.
        /// </summary>
        public const string CreateUser = CommonApiUrl + "Users/CreateUser/{include}";

        public const string UploadFile = CommonApiUrl + "StoredFile/Upload";

        public const string CurrentUserInfo = CommonApiUrl + "Accounts/CurrentUserInfo";


        public const string GetUser = CommonApiUrl + "Users/GetUser";

        public const string GetTheUserAddresses = CommonApiUrl + "Users/GetTheUserAddresses";

        public const string DeleteAddress = CommonApiUrl + "Users/DeleteAddress";

        public const string UpdateAddress = CommonApiUrl + "Users/UpdateAddress";
        public const string AddNewAddress = CommonApiUrl + "Users/AddNewAddress";

        public const string UpdateUser = CommonApiUrl + "Users/UpdateUser";

        public const string UploadUserProfileImg = CommonApiUrl + "Users/UploadUserProfileImg";

        public const string GetContactWithUserId = CommonApiUrl + "Users/GetContactWithUserId";

        /// <summary>
        /// Get Quotations
        /// </summary>

        //public const string GetQuotationByUserId = CommonApiUrl + "Quotations/GetQuotationByUserId?offset=0&size=10";





        public const string DeleteUser = CommonApiUrl + "Users/DeleteUser";
        #region Logs

        public const string Logging = CommonApiUrl + "Logs/Post";
        #endregion 
        #region UserWallet
        public const string GetUserBalanceState = CommonApiUrl + "UserWallet/GetUserBalanceState";

        public const string AddCreditCard = CommonApiUrl + "UserWallet/GetUserBalanceState";
     
        protected Config()
        {
        }
        #endregion
        #region Quotation
        public const string GetQuotationByUserId = CommonApiUrl + "Quotations/GetQuotationByUserId?offset={0}&size={1}";
        public const string QuotationAccpetReject = CommonApiUrl + "Quotations/QuotationAccpetReject";
        //QuotationAccpetReject
        #endregion 
        /// <summary>
        /// Accounting
        /// </summary>
        #region Invoices
        public const string GetUserInvoices = CommonApiUrl + "Invoices/GetInvoicesByUserId?offset={0}&size={1}";
        //GenerateInvoice
        public const string GenerateInvoice = CommonApiUrl + "Invoices/GenerateInvoice";
        public const string SendInvoicePdf = CommonApiUrl + "Invoices/SendInvoicePdf";
        #endregion
        public static void ShowDialog()
        {
            UserDialogs.Instance.ShowLoading();
        }

        public static void HideDialog()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}