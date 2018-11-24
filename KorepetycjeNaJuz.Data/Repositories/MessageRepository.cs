using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Infrastructure.Repositories
{
    class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly KorepetycjeContext _context;

        public MessageRepository(KorepetycjeContext context):
            base(context)
        {
            _context = context;
        }

        public Task<List<Message>> GetConversationWithUserAsync(int user1Id, int user2Id)
        {
            return _context.Messages.AsNoTracking()
                .Where(m => m.OwnerId == user1Id && m.RecipientId == user2Id || m.OwnerId == user2Id && m.RecipientId == user1Id)
                .ToListAsync();
        }

        public Task<Dictionary<int, User>> GetInterlocutorsAsync(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Message>> GetUserMessagesAsync(int userId)
        {
            return _context.Messages.AsNoTracking().Where(m => m.RecipientId == userId || m.OwnerId == userId).ToListAsync();
        }
    }
}
