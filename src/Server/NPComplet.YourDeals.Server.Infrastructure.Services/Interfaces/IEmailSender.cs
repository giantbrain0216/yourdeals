#region

using System.Threading.Tasks;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendHtmlEmailAsync(string toAddress, string subject, string body);
    }
}