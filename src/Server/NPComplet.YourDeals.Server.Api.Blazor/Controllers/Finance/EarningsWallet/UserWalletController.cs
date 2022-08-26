using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPComplet.YourDeals.Domain.Shared.Finance.EarningsWallet;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Finance.EarningsWallet
{
    /// <summary>
    /// 
    /// </summary>
    public class UserWalletController : TypeBaseController<UserEarningsWallet>
    {
        private readonly IFinanceOperationService _financeOpreationService;
        private readonly ICyberSorurceFiananceGateWay _cyberSorurceFiananceGateWay;
        private readonly IUserEarningsWalletService _userEarningsWalletService;
     /// <summary>
     /// 
     /// </summary>
     /// <param name="userManager"></param>
     /// <param name="unitOfWork"></param>
     /// <param name="logger"></param>
     /// <param name="financeOpreationService"></param>
     /// <param name="cyberSorurceFiananceGateWay"></param>
     /// <param name="userEarningsWalletService"></param>
        public UserWalletController(UserManager<User> userManager, IUnitOfWork unitOfWork, ILogger<UserWalletController> logger, IFinanceOperationService financeOpreationService, ICyberSorurceFiananceGateWay cyberSorurceFiananceGateWay, IUserEarningsWalletService userEarningsWalletService)
           : base(userManager, unitOfWork, logger)
        {
            _financeOpreationService = financeOpreationService;
            _cyberSorurceFiananceGateWay = cyberSorurceFiananceGateWay;
            _userEarningsWalletService = userEarningsWalletService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserBalanceState()
        {
            var userid = this.User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier)?.Value;
            var UserBalanceState = await _userEarningsWalletService.GetUserBalance(Guid.Parse(userid));

            if(UserBalanceState!=null)
            {
                return Ok(await Result<UserEarningsWallet>.SuccessAsync(UserBalanceState));
            }
            else
            {
                return Ok(await Result<UserEarningsWallet>.FailAsync());
            }

          
        }

        }
    }
