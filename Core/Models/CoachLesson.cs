using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.Models
{
    public class CoachLesson
    {
        [Key]
        public int Id { get; set; }

        public int CoachId { get; set; }
        public User Coach { get; set; }

        public int LessonSubjectId { get; set; }
        public LessonSubject Subject { get; set; }

        public int LessonLevelId { get; set; }
        public LessonLevel LessonLevel { get; set; }

        public decimal RatePerHour { get; set; }
    }
}
