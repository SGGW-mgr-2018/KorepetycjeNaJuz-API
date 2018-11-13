using KorepetycjeNaJuz.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.Models
{
    public class Lesson : IEntityWithTypedId<int>
    {
        [Key]
        public int Id { get; set; }

        public int LessonStatusId { get; set; }
        public virtual LessonStatus LessonStatus { get; set; }

        public int CoachLessonId { get; set; }
        public virtual CoachLesson CoachLesson { get; set; }

        public int StudentId { get; set; }
        public virtual User Student { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public float NumberOfHours { get; set; }

        public byte? RatingOfStudent { get; set; }

        [MaxLength(2000)]
        public string OpinionOfStudent { get; set; }

        public byte? RatingOfCoach { get; set; }

        [MaxLength(2000)]
        public string OpinionOfCoach { get; set; }
    }
}
