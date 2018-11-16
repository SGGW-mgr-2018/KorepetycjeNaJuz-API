using System;
using System.Collections.Generic;
using System.Text;

namespace KorepetycjeNaJuz.Core.Models
{
    public class CoachLessonLevel
    {
        public int CoachLessonId { get; set; }
        public virtual CoachLesson CoachLesson { get; set; }

        public int LessonLevelId { get; set; }
        public virtual LessonLevel LessonLevel { get; set; }
    }
}
