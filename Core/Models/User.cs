using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.Models
{
    public class User : IdentityUser<int>, Interfaces.IEntityWithTypedId<int>
    {
        [Key]
        public override int Id { get; set; }

        [MaxLength(255)]
        public string FirstName { get; set; }

        [MaxLength(255)]
        public string LastName { get; set; }

        [MaxLength(255)]
        public override string Email { get; set; }

        [Phone]
        [MaxLength(15)]
        public string Telephone { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [MaxLength(1000)]
        public byte[] Avatar { get; set; }

        public bool RodoConfirmed { get; set; }

        public bool CookiesConfirmed { get; set; }

        public bool PrivacyPolicesConfirmed { get; set; }
    }
}
