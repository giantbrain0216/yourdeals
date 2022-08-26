using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Models;

namespace NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Views.Emails.ResendAccountVerification
{
    public class ResendAccountConfirmationViewModel : IRazorHtmlModel
    {
        public ResendAccountConfirmationViewModel(string Id, string token, string redirectUrl)
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
