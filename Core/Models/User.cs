using KorepetycjeNaJuz.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public string Avatar { get; set; }

        public bool RodoConfirmed { get; set; }

        public bool CookiesConfirmed { get; set; }

        public bool PrivacyPolicesConfirmed { get; set; }

        public float CoachRating { get; set; }

        public virtual ICollection<CoachLesson> CoachLessonsAsTeacher { get; set; }

        public override bool Equals(object obj)
        {
            return (obj is User user) && user.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
