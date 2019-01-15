namespace KorepetycjeNaJuz.Core.DTO
{
    public class UserDTO
    {
        /// <summary>
        /// Id użytkownika
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Imię użytkownika
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Nazwisko użytkownika
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Adres email użytkownika
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Numer telefonu użytkownika
        /// </summary>
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
        /// Opisuje średnią ocen użytkownika 
        /// jako korepetytora (jeśli nie prowadził lekcji ma 0)
        /// brane są pod uwagę tylko te lekcje, które mają status Approved 
        /// oraz mają wystawioną ocenę
        /// </summary>
        public float CoachRating { get; set; }
    }
}
