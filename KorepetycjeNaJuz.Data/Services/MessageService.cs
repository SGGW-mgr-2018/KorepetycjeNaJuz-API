using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Infrastructure.Services
{
    public class MessageService : IMessageService
    {
        private IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<List<Message>> GetConversationWithUserAsync(int user1Id, int user2Id)
        {
            return await _messageRepository.GetConversationWithUserAsync(user1Id, user2Id);
        }

        public async Task<IEnumerable<IGrouping<int, Message>>> GetConversation(int userId)
        {
            return (await _messageRepository.GetUserMessagesAsync(userId)).GroupBy(m => m.OwnerId == userId ? m.RecipientId : m.OwnerId);
        }

        public async Task<Dictionary<int, User>> GetInterlocutorsAsync(int userId)
        {
            return await _messageRepository.GetInterlocutorsAsync(userId);
        }

        public async Task AddMessageAsync(Message message)
        {
            message.Content = message.Content.Trim().TrimEnd(new char[] { '\r', '\n', });
            await _messageRepository.AddAsync(message);
        }

        public async Task<Message> GetMessageAsync(int id)
        {
            return await _messageRepository.GetByIdAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            await _messageRepository.DeleteAsync(id);
        }
    }
}
