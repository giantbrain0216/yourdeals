using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceSupports;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Finance.FinanceSupports.CreditCards
{
    /// <summary>
    /// 
    /// </summary>
    public class CreditCardsController : FinanceSupportsController<CreditCard>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="financeService"></param>
        /// <param name="cyberSorurceFiananceGateWay"></param>
        public CreditCardsController(UserManager<User> userManager, IUnitOfWork unitOfWork, ILogger<CreditCardsController> logger, IFinanceService financeService, ICyberSorurceFiananceGateWay cyberSorurceFiananceGateWay)
           : base(userManager, unitOfWork, logger,financeService,cyberSorurceFiananceGateWay)
        {
           
        }
    }
}
