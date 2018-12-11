using System;
using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class LessonRatingDTO
    {
        public LessonRatingDTO() { }
        public LessonRatingDTO(int id, int rating, string opinion)
        {
            LessonId = id;
            Rating = rating;
            Opinion = opinion;
        }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Pole musi być liczbą całkowitą większą niż 0.")]
        public int LessonId { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane.")]
        [Range(1, 5, ErrorMessage = "Pole musi być liczbą całkowitą z zakresu 1-5.")]
        public int Rating { get; set; }

        [MaxLength(2000, ErrorMessage = "Komentarz może zawierać maksymalnie 2000 znaków.")]
        public string Opinion { get; set; }
    }
}
