using System.Threading.Tasks;

namespace NPComplet.YourDeals.Server.Infrastructure.RazorHtmlEmails.Service
{
    public interface IRazorViewToStringRenderer
    {
        Task<string> RenderViewToStringAsync<TRazorHtmlModel>(string viewName, TRazorHtmlModel model);
    }
}