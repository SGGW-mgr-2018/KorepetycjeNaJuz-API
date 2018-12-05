using System;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class LessonDTO
    {
        public int Id { get; set; }

        public int LessonStatusId { get; set; }

        public UserDTO Student { get; set; }

        public DateTime Date { get; set; }

        public float NumberOfHours { get; set; }

        public byte? RatingOfStudent { get; set; }

        public string OpinionOfStudent { get; set; }

        public byte? RatingOfCoach { get; set; }

        public string OpinionOfCoach { get; set; }
    }
}
