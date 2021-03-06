﻿using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class UserCreateDTO
    {
        /// <summary>
        /// Imię użytkownika
        /// </summary>
        [Required(ErrorMessage = "Imię jest wymagane.")]
        [MinLength(3, ErrorMessage = "Imię musi zawierać co najmniej 3 znaki.")]
        [MaxLength(255, ErrorMessage = "Imię może zawierać maksymalnie 255 znaków.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Nazwisko użytkownika
        /// </summary>
        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [MinLength(3, ErrorMessage = "Nazwisko musi zawierać co najmniej 3 znaki.")]
        [MaxLength(255, ErrorMessage = "Nazwisko może zawierać maksymalnie 255 znaków.")]
        public string LastName { get; set; }

        /// <summary>
        /// Nowe hasło użytkownika
        /// </summary>
        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Hasło musi zawierać co najmniej 8 znaków, mała i wielką literę, liczbę oraz znak specjalny.")]
        public string Password { get; set; }

        /// <summary>
        /// Email użytkownika
        /// </summary>
        [DataType(DataType.EmailAddress, ErrorMessage = "To nie wygląda na poprawny adres email.")]
        [EmailAddress(ErrorMessage = "To nie wygląda na poprawny adres email.")]
        [Required(ErrorMessage = "Email jest wymagany.")]
        public string Email { get; set; }

        /// <summary>
        /// Numer telefonu użytkownika
        /// </summary>
        [RegularExpression(@"^(?:\(?\+?48)?(?:[-\.\(\)\s]*(\d)){9}\)?$", ErrorMessage = "To nie jest poprawny numer telefonu.")]
        public string Telephone { get; set; }

        /// <summary>
        /// Opis użytkownika - sekcja 'O mnie'
        /// </summary>s
        public string Description { get; set; }

        /// <summary>
        /// Akceptacja regulaminu serwisu
        /// </summary>
        [Range(typeof(bool), "true", "true", ErrorMessage = "Musisz zaakceptować regulamin serwisu.")]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public bool PrivacyPolicesConfirmed { get; set; }

        /// <summary>
        /// Zdjęcie/avatar użytkownika (base64 image)
        /// </summary>
        public string Avatar { get; set; }
    }
}
