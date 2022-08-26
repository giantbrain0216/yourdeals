using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Models;

namespace NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Views.Emails.ConfirmAccount
{
    public class AccountGenericEmailViewModel : IRazorHtmlModel
    {
        public AccountGenericEmailViewModel(string Id, string confirmEmailUrl , string confirmEmailMessage, string welcome, string resetMessage,string assistantMessage)
        {
            ID = Id;
            EmailUrl = confirmEmailUrl;
            MsgBody = resetMessage;
            AssistantMessage = assistantMessage;
            Welcome = welcome;
            EmailUrlMessage = confirmEmailMessage;
        }

        public string ID { get; set; }
        public string EmailUrl { get; set; }
        public string MsgBody { get; set; }
        public string AssistantMessage { get; set; }
        public string Welcome { get; set; }
        public string EmailUrlMessage { get; set; }


    }
}
