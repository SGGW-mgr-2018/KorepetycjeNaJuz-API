using KorepetycjeNaJuz.Core.Attributes.ValidationsAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class UserEditDTO
    {
        /// <summary>
        /// Id edytowanego użytkownika 
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Pole musi być liczbą całkowitą większą niż 0.")]
        public int Id { get; set; }

        /// <summary>
        /// Imię użytkownika
        /// </summary>
        [MinLength(3, ErrorMessage = "Imię musi zawierać co najmniej 3 znaki.")]
        [MaxLength(255, ErrorMessage = "Imię może zawierać maksymalnie 255 znaków.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Nazwisko użytkownika
        /// </summary>
        [MinLength(3, ErrorMessage = "Nazwisko musi zawierać co najmniej 3 znaki.")]
        [MaxLength(255, ErrorMessage = "Nazwisko może zawierać maksymalnie 255 znaków.")]
        public string LastName { get; set; }

        /// <summary>
        /// Nowe hasło użytkownika
        /// </summary>
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Hasło musi zawierać co najmniej 8 znaków, mała i wielką literę, liczbę oraz znak specjalny.")]
        public string Password { get; set; }

        /// <summary>
        /// Numer telefonu użytkownika
        /// </summary>
        [RegularExpression(@"^(?:\(?\+?48)?(?:[-\.\(\)\s]*(\d)){9}\)?$", ErrorMessage = "To nie jest poprawny numer telefonu.")]
        public string Telephone { get; set; }

        /// <summary>
        /// Opis użytkownika - sekcja 'O mnie'
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Zdjęcie/avatar użytkownika (base64 image)
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// Akceptacja regulaminu serwisu
        /// </summary>
        [RequireValueIfExists("True", ErrorMessage = "Musisz zaakceptować regulamin serwisu.")]
        public bool? PrivacyPolicesConfirmed { get; set; }
    }
}
