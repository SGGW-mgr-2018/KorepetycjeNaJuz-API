using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class LessonAcceptDTO
    {
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Pole musi być liczbą całkowitą większą niż 0.")]
        public int CoachLessonId { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Pole musi być liczbą całkowitą większą niż 0.")]
        public int LessonId { get; set; }
    }
}
