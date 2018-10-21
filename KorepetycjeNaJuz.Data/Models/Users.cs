﻿using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Data.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string FirstName { get; set; }

        [MaxLength(255)]
        public string LastName { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        [Phone]
        [MaxLength(15)]
        public string Telephone { get; set; }

        public bool IsCoach { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [MaxLength(1000)]
        public byte[] Avatar { get; set; }
    }
}
