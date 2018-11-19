using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class UserCreateDTO
    {
        [Required(ErrorMessage = "Imię jest wymagane.")]
        [MinLength(3, ErrorMessage = "Imię musi zawierać co najmniej 3 znaki.")]
        [MaxLength(255, ErrorMessage = "Imię może zawierać maksymalnie 255 znaków.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [MinLength(3, ErrorMessage = "Nazwisko musi zawierać co najmniej 3 znaki.")]
        [MaxLength(255, ErrorMessage = "Nazwisko może zawierać maksymalnie 255 znaków.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Hasło musi zawierać co najmniej 8 znaków, mała i wielką literę, liczbę oraz znak specjalny.")]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "To nie wygląda na poprawny adres email.")]
        [EmailAddress(ErrorMessage = "To nie wygląda na poprawny adres email.")]
        [Required(ErrorMessage = "Email jest wymagany.")]
        public string Email { get; set; }

        [RegularExpression(@"^(?:\(?\+?48)?(?:[-\.\(\)\s]*(\d)){9}\)?$", ErrorMessage = "To nie jest poprawny numer telefonu.")]
        public string Telephone { get; set; }

        public string Description { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Musisz zaakceptować regulamin serwisu.")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public bool PrivacyPolicesConfirmed { get; set; }
    }
}
