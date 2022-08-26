#region

using System;
using System.Threading.Tasks;
using CyberSource.Api;
using CyberSource.Client;
using CyberSource.Model;
using Microsoft.Extensions.Logging;
using NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Cybersource.Specifications;
using NPComplet.YourDeals.Server.Infrastructure.Services.ConfigurationSettings;
using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Classes
{
    public class CyberSourceFiananceGateWay : ICyberSorurceFiananceGateWay
    {
        private readonly ILogger<CyberSourceFiananceGateWay> _logger;

        public CyberSourceFiananceGateWay(ILogger<CyberSourceFiananceGateWay> logger)
        {
            _logger = logger;
        }

        public Task<CybersourcePaymentResponse> SimplePaymentWithCredidtCard(CybersourcePaymentRequest cyberrequest)
        {
            var clientReferenceInformation = new Ptsv2paymentsClientReferenceInformation(
                TransactionId: cyberrequest.FiniancialOpreationId.ToString()
            );

            var processingInformationCapture = false;
            if (cyberrequest.CaptureTrueForProcessPayment) processingInformationCapture = true;

            var processingInformation = new Ptsv2paymentsProcessingInformation(
                Capture: processingInformationCapture
            );


            var paymentInformationCard = new Ptsv2paymentsPaymentInformationCard(
                cyberrequest.CreditCard.Number,
                cyberrequest.CreditCard.ExpireMonth.ToString(),
                cyberrequest.CreditCard.ExpireYear.ToString(), SecurityCode: cyberrequest.CreditCard.Cvv2
            );
            ;

            var paymentInformation = new Ptsv2paymentsPaymentInformation(
                paymentInformationCard
            );

            var orderInformationAmountDetails = new Ptsv2paymentsOrderInformationAmountDetails(
                cyberrequest.FinanceOperation.Amount.AmountValue.ToString(),
                "EUR"
            );

            var orderInformationBillToFirstName = cyberrequest.Owner.FirstName;
            var orderInformationBillToLastName = cyberrequest.Owner.LastName;
            var orderInformationBillToAddress1 = "";
            var orderInformationBillToLocality = "";
            var orderInformationBillToAdministrativeArea = "";
            var orderInformationBillToPostalCode = "";
            var orderInformationBillToCountry = "";
            var orderInformationBillToEmail = cyberrequest.Owner.Email;
            var orderInformationBillToPhoneNumber = "";
            var orderInformationBillTo = new Ptsv2paymentsOrderInformationBillTo(
                orderInformationBillToFirstName,
                orderInformationBillToLastName,
                Address1: orderInformationBillToAddress1,
                Locality: orderInformationBillToLocality,
                AdministrativeArea: orderInformationBillToAdministrativeArea,
                PostalCode: orderInformationBillToPostalCode,
                Country: orderInformationBillToCountry,
                Email: orderInformationBillToEmail,
                PhoneNumber: orderInformationBillToPhoneNumber
            );

            var orderInformation = new Ptsv2paymentsOrderInformation(
                orderInformationAmountDetails,
                orderInformationBillTo
            );

            var requestObj = new CreatePaymentRequest(
                clientReferenceInformation,
                processingInformation,
                PaymentInformation: paymentInformation,
                OrderInformation: orderInformation
            );

            try
            {
                var configDictionary = new CysberSorurceFiananceGateWaySetting().GetConfiguration();
                var clientConfig = new Configuration(merchConfigDictObj: configDictionary);

                var apiInstance = new PaymentsApi(clientConfig);
                var result = apiInstance.CreatePayment(requestObj);
                var appresponse = new CybersourcePaymentResponse();
                appresponse.OwnerId = cyberrequest.FinanceOperation.OwnerId;
                appresponse.FiniancialOpreationId = cyberrequest.FiniancialOpreationId;
                return Task.FromResult(appresponse);
            }
            catch (ApiException err)
            {
                Console.WriteLine("Error Code: " + err.ErrorCode);
                Console.WriteLine("Error Message: " + err.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception on calling the API : " + e.Message);
                return null;
            }
        }


        public Task<CybersourcePayoutResponse> SimplePayoutWithNotTokenCard(CybersourcePayoutRequest financeOperation)
        {
            var clientReferenceInformation = new Ptsv2payoutsClientReferenceInformation(
                financeOperation.FiniancialOpreationId.ToString()
            );
            var orderInformationAmountDetails = new Ptsv2payoutsOrderInformationAmountDetails(
                financeOperation.FinanceOperation.Amount.AmountValue.ToString(),
                "EUR"
            );

            var orderInformation = new Ptsv2payoutsOrderInformation(
                orderInformationAmountDetails
            );

            var merchantInformationMerchantDescriptorName = "Sending Company Name";
            var merchantInformationMerchantDescriptorLocality = "";
            var merchantInformationMerchantDescriptorCountry = "";
            var merchantInformationMerchantDescriptorAdministrativeArea = "";
            var merchantInformationMerchantDescriptorPostalCode = "";
            var merchantInformationMerchantDescriptor = new Ptsv2payoutsMerchantInformationMerchantDescriptor(
                merchantInformationMerchantDescriptorName,
                merchantInformationMerchantDescriptorLocality,
                merchantInformationMerchantDescriptorCountry,
                merchantInformationMerchantDescriptorAdministrativeArea,
                merchantInformationMerchantDescriptorPostalCode
            );

            var merchantInformation = new Ptsv2payoutsMerchantInformation(
                MerchantDescriptor: merchantInformationMerchantDescriptor
            );

            var recipientInformationFirstName = financeOperation.Owner.FirstName;
            var recipientInformationLastName = financeOperation.Owner.LastName;
            var recipientInformationAddress1 = "";
            var recipientInformationLocality = "";
            var recipientInformationAdministrativeArea = "";
            var recipientInformationCountry = "";
            var recipientInformationPostalCode = "";
            var recipientInformationPhoneNumber = "";
            var recipientInformation = new Ptsv2payoutsRecipientInformation(
                recipientInformationFirstName,
                LastName: recipientInformationLastName,
                Address1: recipientInformationAddress1,
                Locality: recipientInformationLocality,
                AdministrativeArea: recipientInformationAdministrativeArea,
                Country: recipientInformationCountry,
                PostalCode: recipientInformationPostalCode,
                PhoneNumber: recipientInformationPhoneNumber
            );

            var senderInformationReferenceNumber = "NPComplet.RightNDeals";
            var senderInformationAccountFundsSource = "05";
            var senderInformationAccount = new Ptsv2payoutsSenderInformationAccount(
                senderInformationAccountFundsSource
            );

            var senderInformationName = "NPComplet.RightNDeals";
            var senderInformationAddress1 = "Paris";
            var senderInformationLocality = "Paris";

            var senderInformationCountryCode = "FR";
            var senderInformation = new Ptsv2payoutsSenderInformation(
                senderInformationReferenceNumber,
                senderInformationAccount,
                Name: senderInformationName,
                Address1: senderInformationAddress1,
                Locality: senderInformationLocality,
                CountryCode: senderInformationCountryCode
            );
            // person to person
            var processingInformationBusinessApplicationId = "PP";
            var processingInformationNetworkRoutingOrder = "V8";
            var processingInformationCommerceIndicator = "internet";
            var processingInformation = new Ptsv2payoutsProcessingInformation(
                processingInformationBusinessApplicationId,
                processingInformationNetworkRoutingOrder,
                processingInformationCommerceIndicator
            );

            var paymentInformationCardType = "001";
            var paymentInformationCardNumber = "4111111111111111";
            var paymentInformationCardExpirationMonth = "12";
            var paymentInformationCardExpirationYear = "2025";
            var paymentInformationCard = new Ptsv2payoutsPaymentInformationCard(
                paymentInformationCardType,
                paymentInformationCardNumber,
                paymentInformationCardExpirationMonth,
                paymentInformationCardExpirationYear
            );

            var paymentInformation = new Ptsv2payoutsPaymentInformation(
                paymentInformationCard
            );

            var requestObj = new OctCreatePaymentRequest(
                clientReferenceInformation,
                orderInformation,
                merchantInformation,
                recipientInformation,
                senderInformation,
                processingInformation,
                paymentInformation
            );

            try
            {
                var configDictionary = new CysberSorurceFiananceGateWaySetting().GetConfiguration();
                var clientConfig = new Configuration(merchConfigDictObj: configDictionary);

                var apiInstance = new PayoutsApi(clientConfig);
                apiInstance.OctCreatePayment(requestObj);
                var appresponse = new CybersourcePayoutResponse();
                appresponse.OwnerId = financeOperation.OwnerId;
                appresponse.FiniancialOpreationId = financeOperation.FiniancialOpreationId;
                return Task.FromResult(appresponse);
            }
            catch (Exception e)
            {
                _logger.LogError("Exception on calling the API : " + e.Message);
                return null;
            }
        }

        public Task<CybersourcePaymentResponse> SimplePaymentWithBankAccount(CybersourcePaymentRequest financeOperation)
        {
            var clientReferenceInformation = new Ptsv2paymentsClientReferenceInformation(
                TransactionId: financeOperation.FiniancialOpreationId.ToString()
            );

            var processingInformationCapture = financeOperation.CaptureTrueForProcessPayment;

            var processingInformation = new Ptsv2paymentsProcessingInformation(
                Capture: processingInformationCapture
            );


            var paymentInformationBank = new Ptsv2paymentsPaymentInformationBank(
                Iban: financeOperation.BankAccount.IBAN
            );

            var paymentInformation = new Ptsv2paymentsPaymentInformation(
                Bank: paymentInformationBank
            );

            var orderInformationAmountDetails = new Ptsv2paymentsOrderInformationAmountDetails(
                financeOperation.FinanceOperation.Amount.AmountValue.ToString(),
                "EUR"
            );

            var orderInformationBillToFirstName = financeOperation.Owner.FirstName;
            var orderInformationBillToLastName = financeOperation.Owner.LastName;
            var orderInformationBillToAddress1 = "";
            var orderInformationBillToLocality = "";
            var orderInformationBillToAdministrativeArea = "";
            var orderInformationBillToPostalCode = "";
            var orderInformationBillToCountry = "";
            var orderInformationBillToEmail = financeOperation.Owner.Email;
            var orderInformationBillToPhoneNumber = "";
            var orderInformationBillTo = new Ptsv2paymentsOrderInformationBillTo(
                orderInformationBillToFirstName,
                orderInformationBillToLastName,
                Address1: orderInformationBillToAddress1,
                Locality: orderInformationBillToLocality,
                AdministrativeArea: orderInformationBillToAdministrativeArea,
                PostalCode: orderInformationBillToPostalCode,
                Country: orderInformationBillToCountry,
                Email: orderInformationBillToEmail,
                PhoneNumber: orderInformationBillToPhoneNumber
            );

            var orderInformation = new Ptsv2paymentsOrderInformation(
                orderInformationAmountDetails,
                orderInformationBillTo
            );

            var requestObj = new CreatePaymentRequest(
                clientReferenceInformation,
                processingInformation,
                PaymentInformation: paymentInformation,
                OrderInformation: orderInformation
            );

            try
            {
                var configDictionary = new CysberSorurceFiananceGateWaySetting().GetConfiguration();
                var clientConfig = new Configuration(merchConfigDictObj: configDictionary);

                var apiInstance = new PaymentsApi(clientConfig);
                var result = apiInstance.CreatePayment(requestObj);
                var appresponse = new CybersourcePaymentResponse();
                appresponse.OwnerId = financeOperation.OwnerId;
                appresponse.FiniancialOpreationId = financeOperation.FiniancialOpreationId;
                return Task.FromResult(appresponse);
            }
            catch (ApiException err)
            {
                Console.WriteLine("Error Code: " + err.ErrorCode);
                Console.WriteLine("Error Message: " + err.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception on calling the API : " + e.Message);
                return null;
            }
        }
    }
}