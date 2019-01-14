using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KorepetycjeNaJuz.Core.DTO.Message
{
    public class ConversationDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Pole musi być liczbą całkowitą większą niż 0.")]
        public int UserId { get; private set; }
        public MessageDTO LastMessage { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int NumberOfUnreadMessages { get; set; }

        public static ConversationDTO Create(IGrouping<int, Models.Message> conversation, Dictionary<int, Models.User> users, int currentUserId)
        {
            return new ConversationDTO
            {
                UserId = conversation.Key,
                LastMessage = new MessageDTO(conversation.OrderByDescending(m => m.DateOfSending).First(), currentUserId),
                FirstName = users[conversation.Key].FirstName,
                LastName = users[conversation.Key].LastName,
                NumberOfUnreadMessages = conversation.Where(x => !x.IsRead && x.OwnerId == conversation.Key).Count()
            };
        }
    }
}