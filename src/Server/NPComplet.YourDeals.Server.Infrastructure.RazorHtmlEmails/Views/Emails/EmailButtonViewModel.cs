namespace NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Views.Emails
{
    public class EmailButtonViewModel
    {
        public EmailButtonViewModel(string Id,string text, string url)
        {
            ID = Id;
            Text = text;
            Url = url;
        }
        public string ID { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
    }
}
