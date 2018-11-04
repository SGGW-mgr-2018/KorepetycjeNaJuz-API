using System;
using System.Collections.Generic;
using System.Text;

namespace KorepetycjeNaJuz.Data.DTO
{
    public class UserDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public bool IsCoach { get; set; }

        public string Description { get; set; }

        public byte[] Avatar { get; set; }
    }
}
