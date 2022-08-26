using System;

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Classes
{
    #region

    #region

    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using MimeKit.Text;
    using ConfigurationSettings;
    using Interfaces;

    #endregion

    #endregion

    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    /// <summary>
    ///     The email sender.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<EmailSettings> _emailSetting;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EmailSender" /> class.
        /// </summary>
        /// <param name="emailSetting">
        ///     The email setting.
        /// </param>
        public EmailSender(IOptions<EmailSettings> emailSetting)
        {
            _emailSetting = emailSetting;
        }


        public async Task<bool> SendHtmlEmailAsync(string to, string subject, string body)
        {
            var mail = new MailMessage();
            // set the addresses
            mail.From = new MailAddress(_emailSetting.Value.PostmasterEmail); // IMPORTANT: This must be same as your smtp authentication address.
            mail.To.Add(to);
            // set the content
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            // send the message using MailKit.Net.Smtp;
            var smtp = new SmtpClient(_emailSetting.Value.SMTPServer,Int32.Parse(_emailSetting.Value.SMTPServerPort));
            // IMPORANT:  Your smtp login email MUST be same as your FROM address.
            var Credentials = new NetworkCredential(_emailSetting.Value.PostmasterEmailAccount, _emailSetting.Value.PostmasterPassword);
            smtp.Credentials = Credentials;
            await Task.Run(() => { smtp.Send(mail); });
            return true;
        }
    }
}