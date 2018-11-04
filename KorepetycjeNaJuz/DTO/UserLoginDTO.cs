using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.DTO
{
    public class UserLoginDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
