using System.Net.Http;

namespace NPComplet.YourDeals.Interact.Helper
{
    public class DealInteract
    {
        private const string URL = "https://sub.domain.com/";
        private string _urlParameters = "?api_key=123";
        private HttpClient client = new();
    }
}