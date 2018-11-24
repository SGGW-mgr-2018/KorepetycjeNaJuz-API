using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KorepetycjeNaJuz.Core.Models;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface IMessageRepository : IRepositoryWithTypedId<Message, int>
    {
        Task<List<Message>> GetConversationWithUserAsync(int user1Id, int user2Id);
        Task<Dictionary<int, User>> GetInterlocutorsAsync(int userId);
        Task<List<Message>> GetUserMessagesAsync(int userId);
    }
}
