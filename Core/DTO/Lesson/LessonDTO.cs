using System;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class LessonDTO
    {
        /// <summary>
        /// Id lekcji
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id statusu lekcji
        /// </summary>
        public int LessonStatusId { get; set; }

        /// <summary>
        /// Uczeń
        /// </summary>
        public UserDTO Student { get; set; }

        /// <summary>
        /// Data rozpoczęcia lekcji
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Czas trwania lekcji
        /// </summary>
        public float NumberOfHours { get; set; }

        /// <summary>
        /// Ocena ucznia
        /// </summary>
        public byte? RatingOfStudent { get; set; }

        /// <summary>
        /// Opinia o uczniu
        /// </summary>
        public string OpinionOfStudent { get; set; }

        /// <summary>
        /// Ocena korepetytora
        /// </summary>
        public byte? RatingOfCoach { get; set; }

        /// <summary>
        /// Opinia o korepetytorze
        /// </summary>
        public string OpinionOfCoach { get; set; }
    }
}
