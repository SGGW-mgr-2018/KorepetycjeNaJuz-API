using System;
using System.Collections.Generic;
using System.Text;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class AddCoachLessonDTO
    {
        /// <summary>
        /// Id lekcji(ogłoszenia)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id korepetytora
        /// </summary>
        public int CoachId { get; set; }

        /// <summary>
        /// Poziomy lekcji(ogłoszenia)
        /// </summary>
        public ICollection<CoachLessonLevelDTO> LessonLevels { get; set; }

        /// <summary>
        /// Id tematu / przedmiotu lekcji(ogłoszenia)
        /// </summary>
        public int LessonSubjectId { get; set; }

        /// <summary>
        /// Stawka za godzinę lekcji(ogłoszenia)
        /// </summary>
        public decimal RatePerHour { get; set; }

        /// <summary>
        /// Data rozpoczęcia lekcji(ogłoszenia)
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Data zakończenia lekcji(ogłoszenia)
        /// </summary>
        public DateTime DateEnd { get; set; }

        /// <summary>
        /// Informacje o adresie lekcji(ogłoszenia)
        /// </summary>
        public AddressDTO Address { get; set; }

        /// <summary>
        /// Informacja o czasie trwania lekcji(ogłoszenia)
        /// </summary>
        public int Time { get; set; }
    }
}
