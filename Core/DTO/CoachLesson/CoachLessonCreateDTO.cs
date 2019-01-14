using KorepetycjeNaJuz.Core.ValidationsAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class CoachLessonCreateDTO
    {
        /// <summary>
        /// Id poziomów lekcji(ogłoszenia)
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [MinElements(1, ErrorMessage = "Przynajmniej jeden poziom lekcji jest wymagany.")]
        public ICollection<int> LessonLevels { get; set; }

        /// <summary>
        /// Id tematu / przedmiotu lekcji(ogłoszenia)
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "Pole musi być liczbą całkowitą większą niż 0.")]
        public int LessonSubjectId { get; set; }

        /// <summary>
        /// Stawka za godzinę lekcji(ogłoszenia)
        /// </summary>
        [Range(1, Double.MaxValue, ErrorMessage = "Pole musi być liczbą większą od 1.")]
        public decimal RatePerHour { get; set; }

        /// <summary>
        /// Opis lekcji
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Data rozpoczęcia lekcji(ogłoszenia)
        /// </summary>
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public DateTime? DateStart { get; set; }

        /// <summary>
        /// Data zakończenia lekcji(ogłoszenia)
        /// </summary>
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public DateTime? DateEnd { get; set; }

        /// <summary>
        /// Informacja o czasie trwania lekcji(ogłoszenia) (minuty)
        /// </summary>
        [Range(30, 180, ErrorMessage = "Czas pojedynczej lekcji musi zawierać się w przedziale od 30 do 180 minut.")]
        public int Time { get; set; }

        /// <summary>
        /// Informacje o adresie lekcji(ogłoszenia)
        /// </summary>
        [Required(ErrorMessage = "To pole jest wymagane.")]
        public AddressDTO Address { get; set; }
    }
}
