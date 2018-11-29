using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KorepetycjeNaJuz.Core.Models;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface IMessageService
    {
        Task<List<Message>> GetConversationWithUserAsync(int user1Id, int user2Id);
        Task<IEnumerable<IGrouping<int, Message>>> GetConversation(int userId);
        Task<Dictionary<int, User>> GetInterlocutorsAsync(int userId);
        Task AddMessageAsync(Message message);
        Task<Message> GetMessageAsync(int id);
        Task RemoveAsync(int id);
    }
}
