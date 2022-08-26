
using NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Extensions;
using NPComplet.YourDeals.Domain.Shared.Communication;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Managers.Communication
{
    public class ChatManager : IChatManager
    {
        private readonly HttpClient _httpClient;

        public ChatManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<IEnumerable<Message>>> GetChatHistoryAsync(string cId)
        {
            var response = await _httpClient.GetAsync(Routes.ChatEndpoint.GetChatHistory(cId));
            var data = await response.ToResult<IEnumerable<Message>>();
            return data;
        }

        public async Task<IResult<IEnumerable<User>>> GetChatUsersAsync()
        {
            var response = await _httpClient.GetAsync(Routes.ChatEndpoint.GetAvailableUsers);
            var data = await response.ToResult<IEnumerable<User>>();
            return data;
        }

        public async Task<IResult> SaveMessageAsync(Message chatHistory)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.ChatEndpoint.SaveMessage, chatHistory);
            var data = await response.ToResult();
            return data;
        }
    }
}