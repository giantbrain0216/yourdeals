using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Handlers;

public class IncludeRequestCredentialsMessageHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        return base.SendAsync(request, cancellationToken);
    }
}