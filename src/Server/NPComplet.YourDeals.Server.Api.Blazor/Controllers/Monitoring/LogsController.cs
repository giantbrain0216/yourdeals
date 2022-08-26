// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogsController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Monitoring
{
    #region

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    using NPComplet.YourDeals.Domain.Shared.DataTransferObjects.Exceptions;
    using NPComplet.YourDeals.Domain.Shared.Monitoring;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
    using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;

    #endregion

    /// <summary>
    ///     The logs controller.
    /// </summary>
    [ApiVersion("1.0")]
    public class LogsController : TypeBaseController<Log>
    {
        private readonly IEmailSender _emailSender;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="emailSender"></param>
        public LogsController(UserManager<User> userManager, IUnitOfWork unitOfWork, ILogger<LogsController> logger, IEmailSender emailSender)
            : base(userManager, unitOfWork, logger)
        {
            _emailSender = emailSender;
        }

        /// <summary>
        ///     Get the count fo logs.
        /// </summary>
        /// <returns>The logs count.</returns>
        [HttpGet]
        public override async Task<ActionResult<long>> Count()
        {
            return this.Ok(await this.UnitOfWork.GetRepository<Log>().CountAsync());
        }

        /// <summary>
        /// Get logs by filters.
        /// </summary>
        /// <param name="indexFrom">
        /// The index form.
        /// </param>
        /// <param name="indexTo">
        /// The index to.
        /// </param>
        /// <param name="orderBy">
        /// The order by filter.
        /// </param>
        /// <param name="include">
        /// The include filter.
        /// </param>
        /// <returns>
        /// Logs.
        /// </returns>
        [HttpGet]
        public new async Task<ActionResult<IEnumerable<Log>>> Get(
            int indexFrom,
            int indexTo,
            string orderBy,
            string include)
        {
            return this.Ok(
                await this.UnitOfWork.GetRepository<Log>().GetAllAsync(
                    indexFrom,
                    indexTo,
                    null,
                    orderBy,
                    include.Split(" ")));
        }

        ///// <summary>
        ///// Post a log
        ///// </summary>
        ///// <param name="value">
        ///// The value.
        ///// </param>
        ///// <returns>
        ///// The <see cref="Task"/>.
        ///// </returns>
        //[HttpPost]
        //[AllowAnonymous]
        //public new async Task<IActionResult> Post([FromBody] Log value)
        //{
        //    var message = string.Format(@"<b>DateTime:</b> {0} </br> <b>Stack:</b>{1} </br> <b>Message:</b> {2}", value.Date, value.Exception, value.Message);

        //   await _emailSender.SendEmailAsync("mouadh.jaber@msn.com,amr.elsherif83@outlook.com,arijjijij@gmail.com,firasjaberc@gmail.com,wael_jm@hotmail.fr,mohammed__sajid@hotmail.fr", "RightnDeals][Important Client Application Message] some issues , please check", message);
        //    return await base.Post(value);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        [HttpPost]
        [AllowAnonymous]
        public new async Task<IActionResult> Post([FromBody] Log value)
        {
            if (value != null)
            {
                var ex = new ApplicationCoreException(value.Message, value.Exception);
                this.Logger.LogError(ex, $"Exception in the Client Side Application : {value.Message}");
                return Ok("Saved");
            }
            return BadRequest("Ignored");


        }
    }
}