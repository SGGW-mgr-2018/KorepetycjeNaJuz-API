using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KorepetycjeNaJuz.Core.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Messages_Owner")]
        public int OwnerId { get; set; }
        public User Owner { get; set; }

        [ForeignKey("Messages_Recipients")]
        public int RecipientId { get; set; }
        public User Recipient { get; set; }

        [MaxLength(1000)]
        public string Content { get; set; }

        public DateTime DateOfSending { get; set; }
    }
}
