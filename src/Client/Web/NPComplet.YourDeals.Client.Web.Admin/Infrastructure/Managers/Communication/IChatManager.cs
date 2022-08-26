
using NPComplet.YourDeals.Domain.Shared.Communication;
using NPComplet.YourDeals.Domain.Shared.Users;
using NPComplet.YourDeals.Domain.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace NPComplet.YourDeals.Client.Web.Admin.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<User>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(Message chatHistory);

        Task<IResult<IEnumerable<Message>>> GetChatHistoryAsync(string cId);
    }
}