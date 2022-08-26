using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPComplet.YourDeals.Domain.Shared.Finance;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.CashIn;
using NPComplet.YourDeals.Domain.Shared.Finance.ServiceGateways.Cybersource.Specifications;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Finance.Payout
{
    /// <summary>
    /// 
    /// </summary>
    public class PayoutController : TypeBaseController<CrossingPayout>
    {
        private readonly IFinanceOperationService _financeOperationService;

        private readonly ICyberSorurceFiananceGateWay _cyberSorurceFiananceGateWay;
       /// <summary>
       /// 
       /// </summary>
       /// <param name="userManager"></param>
       /// <param name="unitOfWork"></param>
       /// <param name="logger"></param>
       /// <param name="financeOperationService"></param>
       /// <param name="cyberSorurceFiananceGateWay"></param>
        public PayoutController(UserManager<User> userManager, IUnitOfWork unitOfWork, ILogger<PayoutController> logger, IFinanceOperationService financeOperationService, ICyberSorurceFiananceGateWay cyberSorurceFiananceGateWay)
            : base(userManager, unitOfWork, logger)
        {
            _financeOperationService = financeOperationService;
            _cyberSorurceFiananceGateWay = cyberSorurceFiananceGateWay;
        }


/// <summary>
/// 
/// </summary>
/// <param name="crossingPayout"></param>
/// <returns></returns>
        [HttpPost]
     
        public async Task<IActionResult> DoPayNow([FromBody] CrossingPayout crossingPayout)
        {
            var defaultservicegateway = await UnitOfWork.GetRepository<ServiceGateway>().SingleOrDefaultAsync(p => p.IsApplicationDefault == true);

            if(defaultservicegateway!=null)
            {
                crossingPayout.ServiceGatewayId = defaultservicegateway.Id;
            }
            await Post(crossingPayout);

            if(crossingPayout.FinanceSupportName == Domain.Shared.Enumerable.FinanceSupportName.CREDITCARD)
            {
                var CybersourcePayoutRequest = await _financeOperationService.PrepareDealPayout(crossingPayout);


                if (await _cyberSorurceFiananceGateWay.SimplePayoutWithNotTokenCard(CybersourcePayoutRequest) is { } cybersourcePayoutReponse)
                {
                    return Ok(await Result<CybersourcePayoutResponse>.SuccessAsync(cybersourcePayoutReponse));
                }
                else
                {
                    return Ok(await Result<CybersourcePayoutResponse>.FailAsync("error"));
                }
            }
            return await Post(crossingPayout);
        }
    }
}
