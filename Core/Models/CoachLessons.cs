using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.Models
{
    public class CoachLessons
    {
        [Key]
        public int Id { get; set; }

        public int CoachId { get; set; }
        public Users Coach { get; set; }

        public int LessonSubjectId { get; set; }
        public LessonSubjects Subject { get; set; }

        public int LessonLevelId { get; set; }
        public LessonLevels LessonLevel { get; set; }

        public decimal RatePerHour { get; set; }
    }
}
