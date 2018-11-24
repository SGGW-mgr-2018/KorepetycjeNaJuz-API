using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Infrastructure.Services
{
    class MessageService : IMessageService
    {
        private IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public Task<List<Message>> GetConversationWithUserAsync(int user1Id, int user2Id)
        {
            return _messageRepository.GetConversationWithUserAsync(user1Id, user2Id);
        }

        public async Task<IEnumerable<IGrouping<int, Message>>> GetConversation(int userId)
        {
            return (await _messageRepository.GetUserMessagesAsync(userId)).GroupBy(m => m.OwnerId == userId ? m.RecipientId : m.OwnerId);
        }

        public Task<Dictionary<int, User>> GetInterlocutorsAsync(int userId)
        {
            return _messageRepository.GetInterlocutorsAsync(userId);
        }

        public Task AddMessageAsync(Message message)
        {
            return _messageRepository.AddAsync(message);
        }

        public Task<Message> GetMessageAsync(int id)
        {
            return _messageRepository.GetByIdAsync(id);
        }

        public Task RemoveAsync(int id)
        {
            return _messageRepository.DeleteAsync(id);
        }
    }
}
