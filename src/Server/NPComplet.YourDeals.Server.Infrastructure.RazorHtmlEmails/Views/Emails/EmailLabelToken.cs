namespace NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Views.Emails
{
    public class EmailLabelToken
    {
        public EmailLabelToken(string Id, string token, string redirectUrl)
        {
            ID = Id;
            Token = token;
            RedirectUrl = redirectUrl;
        }

        public string ID { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string RedirectUrl { get; set; }

    }
}
