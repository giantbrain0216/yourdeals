using NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Models;

namespace NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Views.Emails.RegisterWithToken
{
    public class RegisterWithTokenEmailViewModel : IRazorHtmlModel
    {
        public RegisterWithTokenEmailViewModel(string Id, string confirmEmailUrl, string confirmEmailMessage, string welcome, string resetMessage, string assistantMessage)
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
