using System.Threading.Tasks;
using Toolbelt.Blazor;

namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Managers.Interceptors
{
    public interface IHttpInterceptorManager : IManager
    {
        void RegisterEvent();

        Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e);

        void DisposeEvent();
    }
}