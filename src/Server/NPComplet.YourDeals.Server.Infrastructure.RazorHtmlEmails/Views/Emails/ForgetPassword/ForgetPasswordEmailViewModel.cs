using NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Models;

namespace NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Views.Emails.ForgetPassword
{
    public class ForgetPasswordEmailViewModel : IRazorHtmlModel
    {
        public ForgetPasswordEmailViewModel(string Id, string emailUrl, string emailUrlMessage, string message, string assistantMessage, string welcome )
        {
            ID = Id;
            EmailUrl = emailUrl;
            EmailUrlMessage = emailUrlMessage;
            Welcome = welcome;
            MsgBody = message;
            AssistantMessage = assistantMessage;
        }
        public string ID { get; set; }
        public string EmailUrl { get; set; }
        public string MsgBody { get; set; }
        public string AssistantMessage { get; set; }
        public string Welcome { get; set; }
        public string EmailUrlMessage { get; set; }



    }
}
