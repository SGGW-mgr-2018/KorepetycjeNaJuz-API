using KorepetycjeNaJuz.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.Models
{
    public class Lesson : IEntityWithTypedId<int>
    {
        [Key]
        public int Id { get; set; }

        public int LessonStatusId { get; set; }
        public LessonStatus LessonStatus { get; set; }

        public int CoachLessonId { get; set; }
        public CoachLesson CoachLesson { get; set; }

        public int StudentId { get; set; }
        public User Student { get; set; }

        public int CoachAddressId { get; set; }
        public CoachAddress CoachAddress { get; set; }

        public System.DateTime Date { get; set; }

        public int NumberOfHours { get; set; }

        public byte? RatingOfStudent { get; set; }

        public string OpinionOfStudent { get; set; }

        public byte? RatingOfCoach { get; set; }

        public string OpinionOfCoach { get; set; }
    }
}
