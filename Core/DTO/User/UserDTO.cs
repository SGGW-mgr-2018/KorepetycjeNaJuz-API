namespace KorepetycjeNaJuz.Core.DTO
{
    public class UserDTO
    {
        /// <summary>
        /// Id użytkownika
        /// </summary>
        public string Id { get; set; }

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
        /// Zdjęcie/avatar użytkownika
        /// </summary>
        public byte[] Avatar { get; set; }
    }
}
