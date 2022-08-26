namespace NPComplet.YourDeals.Server.Infrastructure.Services.ConfigurationSettings
{
    public class EmailSettings
    {
        public string SMTPServer { get; set; }
        public string SMTPServerPort { get; set; }

        public string PostmasterEmail { get; set; }
        public string PostmasterEmailAccount { get; set; }
        public string PostmasterPassword { get; set; }
    }
}