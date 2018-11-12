using KorepetycjeNaJuz.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace KorepetycjeNaJuz.Core.Models
{
    public class CoachLesson : IEntityWithTypedId<int>
    {
        [Key]
        public int Id { get; set; }

        public int CoachId { get; set; }
        public virtual User Coach { get; set; }

        public int LessonStatusId { get; set; }
        public virtual LessonStatus LessonStatus { get; set; }

        public int LessonSubjectId { get; set; }
        public virtual LessonSubject Subject { get; set; }

        public int LessonLevelId { get; set; }
        public virtual LessonLevel LessonLevel { get; set; }

        public decimal RatePerHour { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateStart { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateEnd { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
