using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KorepetycjeNaJuz.Core.DTO.Message
{
    public class ConversationDTO
    {
        public ConversationDTO(IGrouping<int, Models.Message> conversation)
        {
            UserId = conversation.Key;
            LastMessage = new MessageDTO(conversation.OrderByDescending(m => m.DateOfSending).First());
        }

        [Range(1, int.MaxValue, ErrorMessage = "Pole musi być liczbą całkowitą większą niż 0.")]
        public int UserId { get; }
        public MessageDTO LastMessage { get; }
    }
}