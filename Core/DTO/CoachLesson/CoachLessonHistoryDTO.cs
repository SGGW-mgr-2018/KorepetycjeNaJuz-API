using System;
using System.Collections.Generic;
using System.Text;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class CoachLessonHistoryDTO
    {
        /// <summary>
        /// Id ogłoszenia 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id lekcji (potrzebne do wystawienia opinii)
        /// </summary>
        public int LessonId { get; set; }

        /// <summary>
        /// Tytuł lekcji
        /// </summary>
        public string SubjectName { get; set; }

        /// <summary>
        /// Nazwy poziomów lekcji
        /// np. 'Liceum podstawa, Liceum rozszerzenie' 
        /// </summary>
        public string LessonLevelsName { get; set; }

        /// <summary>
        /// Opis lekcji
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Imię korepetytora
        /// </summary>
        public string CoachFirstName { get; set; }

        /// <summary>
        /// Nazwisko korepetytora
        /// </summary>
        public string CoachLastName { get; set; }

        /// <summary>
        /// Data rozpoczęcia lekcji
        /// </summary>
        public DateTime DateStart { get; set; }

        /// <summary>
        /// Czas trwania lekcji (minuty)
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// Ocena o studencie 
        /// </summary>
        public byte? RatingOfStudent { get; set; }

        /// <summary>
        /// Ocena o korepetytorze
        /// </summary>
        public byte? RatingOfCoach { get; set; }

        /// <summary>
        /// Opinia o studencie
        /// </summary>
        public string OpinionOfStudent { get; set; }

        /// <summary>
        /// Opinia o korepetytorze
        /// </summary>
        public string OpinionOfCoach { get; set; }
    }
}
