// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvoicesController.cs" company="NPComplet">
//   Org
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace NPComplet.YourDeals.Server.Api.Blazor.Controllers.Accounting
{
    #region

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using NPComplet.YourDeals.Server.Infrastructure.Services.ConfigurationSettings;
    using NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces;
    using NPComplet.YourDeals.Domain.Shared.Accounting;
    using NPComplet.YourDeals.Domain.Shared.Users;
    using NPComplet.YourDeals.Domain.Shared.Wrapper;
    using NPComplet.YourDeals.Server.Infrastructure.Repositories.Interfaces;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    ///     the invoices controller
    /// </summary>
    [ApiVersion("1.0")]
    public class InvoicesController : TypeBaseController<Invoice>
    {
        private readonly IEmailSender _emailSender;
        private readonly IBackgroundJobService _backgroundJobService;
        private readonly IOptions<EmailSettings> _emailSetting;
        /// <summary>
        /// Initializes a new instance of the <see cref="InvoicesController"/> class.
        ///     invoice controller
        /// </summary>
        /// <param name="userManager">
        /// </param>
        /// <param name="unitOfWork">
        /// </param>
        /// <param name="logger">
        /// </param>
        public InvoicesController(
            UserManager<User> userManager,
            IUnitOfWork unitOfWork,
            IEmailSender emailSender,
            IOptions<EmailSettings> emailSetting,
            IBackgroundJobService backgroundJobService,
            ILogger<InvoicesController> logger)
            : base(userManager, unitOfWork, logger)
        {
            _emailSender = emailSender;
            _backgroundJobService = backgroundJobService;
            _emailSetting = emailSetting;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<IEnumerable<Invoice>>> GetInvoicesByUserId([FromQuery] int offset, [FromQuery] int size)
        {
            if (size == 0)
            {
                size = 10;
            }
            var userId = UserManager.GetUserId(HttpContext.User);
            Guid.TryParse(userId, out var guid);
            var quotations =
                await UnitOfWork.GetRepository<Invoice>().GetAllAsync(offset,
                size,
                u => u.PurchaserId == guid);

            if (quotations != null)
                return await Result<IEnumerable<Invoice>>.SuccessAsync(quotations);
            else
                return await Result<IEnumerable<Invoice>>.FailAsync("Invoice error");





        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="quotation"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GenerateInvoice([FromBody] Domain.Shared.Accounting.Quotation quotation)
        {

            var userId = UserManager.GetUserId(HttpContext.User);
            Guid.TryParse(userId, out var guid);

            return await Post(new Invoice
            {
                Id = Guid.NewGuid(),
                ClientAddressId = quotation.ClientAddressId,
                ClientAddress = quotation.ClientAddress,
                // CompanyId = quotation.ProposerId,
                Address = quotation.Address,
                AddressId = quotation.AddressId,
                //Amounts = quotation.Amounts,
                PurchaserId = quotation.ProposerId,
                CompanyId = quotation.CompanyId,
                ClientName = quotation.ClientName,
                Company = quotation.Company,
                InternalApplicationName = quotation.InternalApplicationName,
                InternalCreationDateTimeUtc = DateTime.UtcNow,
                OwnerId = quotation.OwnerId,
                PostId = quotation.PostId,

            });

        }
        [HttpGet]
        public async Task<Result<bool>> SendInvoicePdf(string email, byte[] bytes)
        {

            try
            {
                string subject = "INVOICE RECEIP";
                var mail = new MailMessage();
                // set the addresses
                mail.From = new MailAddress(_emailSetting.Value.PostmasterEmail); // IMPORTANT: This must be same as your smtp authentication address.
                mail.To.Add(email);
                // set the content
                mail.Subject = subject;
                mail.Body = "INVOICE RECEIPT";
                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), "Invoice.pdf"));
                mail.IsBodyHtml = true;
                // send the message using MailKit.Net.Smtp;
                var smtp = new SmtpClient(_emailSetting.Value.SMTPServer, Int32.Parse(_emailSetting.Value.SMTPServerPort));
                // IMPORANT:  Your smtp login email MUST be same as your FROM address.
                var Credentials = new NetworkCredential(_emailSetting.Value.PostmasterEmailAccount, _emailSetting.Value.PostmasterPassword);
                smtp.Credentials = Credentials;
                await Task.Run(() => { smtp.Send(mail); });
                return await Result<bool>.SuccessAsync(true);
            }
            catch (Exception ex)
            {
                throw;

            }
        }
    }
}