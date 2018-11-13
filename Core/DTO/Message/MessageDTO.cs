using System;

namespace KorepetycjeNaJuz.Core.DTO.Message
{
    public class MessageDTO
    {
        public MessageDTO(Models.Message m)
        {
            RecipientId = m.RecipientId;
            Content = m.Content;
            OwnerId = m.OwnerId;
            DateOfSending = m.DateOfSending;
        }

        public int RecipientId { get; set; }
        public string Content { get; set; }
        public int OwnerId { get; private set; }
        public DateTime DateOfSending { get; private set; }
    }
}
