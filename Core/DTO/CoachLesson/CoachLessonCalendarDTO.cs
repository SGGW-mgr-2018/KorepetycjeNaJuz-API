using KorepetycjeNaJuz.Core.Enums;
using KorepetycjeNaJuz.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class CoachLessonCalendarDTO
    {
        /// <summary>
        /// Id lekcji(ogłoszenia)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Rola zalogowanego użytkownika
        /// 1 - Uczeń
        /// 2 - Korepetytor
        /// </summary>
        public CoachLessonRole UserRole { get; set; }

        /// <summary>
        /// Id korepetytora
        /// </summary>
        public int CoachId { get; set; }

        /// <summary>
        /// Imię korepetytora
        /// </summary>
        public string CoachFirstName { get; set; }

        /// <summary>
        /// Nazwisko korepetytora
        /// </summary>
        public string CoachLastName { get; set; }

        /// <summary>
        /// Status lekcji(ogłoszenia)
        /// </summary>
        public string LessonStatusName { get; set; }

        /// <summary>
        /// Poziomy lekcji(ogłoszenia)
        /// </summary>
        public ICollection<CoachLessonLevelDTO> LessonLevels { get; set; }

        /// <summary>
        /// Id tematu / przedmiotu lekcji(ogłoszenia)
        /// </summary>
        public int LessonSubjectId { get; set; }

        /// <summary>
        /// Nazwa tematu / przedmiotu lekcji(ogłoszenia)
        /// </summary>
        public string LessonSubject { get; set; }

        /// <summary>
        /// Stawka za godzinę lekcji(ogłoszenia)
        /// </summary>
        public decimal RatePerHour { get; set; }

        /// <summary>
        /// Opis lekcji
        /// </summary>
        public string Description { get; set; }

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

        /// <summary>
        /// Moje zgłoszenie na lekcję (CoachLesson)
        /// Jeśli Rola korepetytor to Null
        /// </summary>
        public LessonDTO MyLesson { get; set; }

        /// <summary>
        /// Lekcje powiązane ze zgłoszeniem
        /// </summary>
        public virtual IEnumerable<LessonDTO> Lessons{ get; set; }
    }
}
