using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.FinanceDTO;
using NPComplet.YourDeals.Domain.Shared.Finance;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceSupports;
using NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Cybersource.Specifications;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Finance.Payment
{
    /// <summary>
    /// /
    /// </summary>
    public class DealPaymentController : TypeBaseController<CrossingPayment>
    {
        private readonly IFinanceOperationService _financeOpreationService;
        private readonly ICyberSorurceFiananceGateWay _cyberSorurceFiananceGateWay;
   /// <summary>
   /// 
   /// </summary>
   /// <param name="userManager"></param>
   /// <param name="unitOfWork"></param>
   /// <param name="logger"></param>
   /// <param name="financeOpreationService"></param>
   /// <param name="cyberSorurceFiananceGateWay"></param>
        public DealPaymentController(UserManager<User> userManager, IUnitOfWork unitOfWork, ILogger<DealPaymentController> logger, IFinanceOperationService financeOpreationService, ICyberSorurceFiananceGateWay cyberSorurceFiananceGateWay)
           : base(userManager, unitOfWork, logger)
        {
            _financeOpreationService = financeOpreationService;
            _cyberSorurceFiananceGateWay = cyberSorurceFiananceGateWay;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUserTransaction([FromQuery] int offset, [FromQuery] int size)
        {
            var userId = UserManager.GetUserId(HttpContext.User);
            var data= await UnitOfWork.GetRepository<CrossingPayment>().GetAllAsync(
                         offset,
                         size,
                         d => d.OwnerId.ToString() == userId && d.InternalValidation,
                        "", "Amount"); 
            if (data.Any())
                return Ok(await Result<IEnumerable<CrossingPayment>>.SuccessAsync(data));
            else
                return Ok(await Result<IEnumerable<CrossingPayment>>.FailAsync("No Transaction yet."));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetTransactionById(Guid Id)
        {
            var trans = await UnitOfWork.GetRepository<CrossingPayment>().SingleOrDefaultAsync(p=>p.Id==Id, "Deal","Proposition" ,"Amount");

            if (trans!=null)
                return Ok(Result<CrossingPayment>.SuccessAsync(trans));
            else
                return Ok(Result<CrossingPayment>.FailAsync("No Transaction found."));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CheckForFinanceSupport()
        {

            var userid = this.User.Claims.FirstOrDefault(p=>p.Type== ClaimTypes.NameIdentifier)?.Value;

            var PrimaryFinanceSupport = await UnitOfWork.GetRepository<FinanceSupport>().SingleOrDefaultAsync(p => p.IsPrimary&&p.OwnerId==Guid.Parse(userid));

            if(PrimaryFinanceSupport!=null)
            {
                return Ok(await Result<FinanceSupport>.SuccessAsync(PrimaryFinanceSupport));
            }
            else
            {
                return Ok(await Result<FinanceSupport>.FailAsync());
            }
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DealPaymentId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DifferentFinanceSupport(Guid DealPaymentId)
        {
            var dealpayment = await UnitOfWork.GetRepository<CrossingPayment>().GetByIdAsync(DealPaymentId);

            if(dealpayment!=null)
            {
                return Ok(await Result<CrossingPayment>.SuccessAsync(dealpayment));
            }
            else
            {
                return Ok(await Result<CrossingPayment>.FailAsync());
            }

        }
     /// <summary>
     /// 
     /// </summary>
     /// <param name="financialOpretion"></param>
     /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DoPaymentWithCreditCard([FromBody] FinancialOpretionDTO  financialOpretion)
        {
          

            var dealpayment = await UnitOfWork.GetRepository<CrossingPayment>().GetByIdAsync(financialOpretion.FinanceOpreationId);
            dealpayment.FinanceSupportId = financialOpretion.SelectedFinanceSupport;
            dealpayment.FinanceSupportName = Domain.Shared.Enumerable.FinanceSupportName.CREDITCARD;
            await Put(financialOpretion.FinanceOpreationId, dealpayment);

            var CybersourcePaymentRequest = await _financeOpreationService.PrepareDealPayment(dealpayment);
         

            var cybersourcePaymentReponse = await _cyberSorurceFiananceGateWay.SimplePaymentWithCredidtCard(CybersourcePaymentRequest);

            if(cybersourcePaymentReponse!=null)
            {
                return Ok(await Result<CybersourcePaymentResponse>.SuccessAsync(cybersourcePaymentReponse));
            }
            else
            {
                return Ok(await Result<CybersourcePaymentResponse>.FailAsync("error"));
            }


        }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="financialOpretion"></param>
    /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DoPaymentWithDifferentBankAccount([FromBody] FinancialOpretionDTO financialOpretion)
        {
            

            var dealpayment = await UnitOfWork.GetRepository<CrossingPayment>().GetByIdAsync(financialOpretion.FinanceOpreationId);
            dealpayment.FinanceSupportId = financialOpretion.SelectedFinanceSupport;
            dealpayment.FinanceSupportName = Domain.Shared.Enumerable.FinanceSupportName.BANKACCOUNT;
            await Put(financialOpretion.FinanceOpreationId, dealpayment);

            var CybersourcePaymentRequest = await _financeOpreationService.PrepareDealPayment(dealpayment);

            var cybersourcePaymentReponse = await _cyberSorurceFiananceGateWay.SimplePaymentWithCredidtCard(CybersourcePaymentRequest);

            if (cybersourcePaymentReponse != null)
            {
                return Ok(await Result<CybersourcePaymentResponse>.SuccessAsync(cybersourcePaymentReponse));
            }
            else
            {
                return Ok(await Result<CybersourcePaymentResponse>.FailAsync("error"));
            }


        }
    }
}
