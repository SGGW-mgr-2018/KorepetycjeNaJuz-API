using Newtonsoft.Json;
using System;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class CoachLessonsByFiltersDTO
    {
        public double? Latitiude { get; set; }

        public double? Longitiude { get; set; }

        public double? Radius { get; set; }

        public int? SubjectId { get; set; }

        public int? LevelId { get; set; }

        public int? CoachId { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public int? StudentId { get; set; }
    }
}
