using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NPComplet.YourDeals.Domain.Shared.Finance.FinanceOperations.Payments;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Finance.Payment
{
    /// <summary>
    /// 
    /// </summary>
    public class PostingDealPaymentController : TypeBaseController<PostingDealPayment>
    
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        public PostingDealPaymentController(UserManager<User> userManager, IUnitOfWork unitOfWork, ILogger<PostingDealPaymentController> logger)
           : base(userManager, unitOfWork, logger)
        {
        }
    }
}
