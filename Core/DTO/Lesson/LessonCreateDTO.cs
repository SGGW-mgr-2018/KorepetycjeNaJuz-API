using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class LessonCreateDTO
    {
        [Required(ErrorMessage = "To pole jest wymagane.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Pole musi być liczbą całkowitą większą niż 0.")]
        public int CoachLessonId { get; set; }

        [JsonIgnore]
        public int StudentId { get; set; }
    }
}
