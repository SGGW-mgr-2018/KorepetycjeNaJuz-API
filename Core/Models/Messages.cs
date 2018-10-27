using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KorepetycjeNaJuz.Core.Models
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Messages_Owner")]
        public int OwnerId { get; set; }
        public Users Owner { get; set; }

        [ForeignKey("Messages_Recipients")]
        public int RecipientId { get; set; }
        public Users Recipient { get; set; }

        [MaxLength(1000)]
        public string Message { get; set; }

        public DateTime DateOfSending { get; set; }
    }
}
