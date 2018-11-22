using Newtonsoft.Json;
using System;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class CoachLessonsByFiltersDTO
    {
        public double? Latitiude { get; set; }

        public double? Longitiude { get; set; }

        public double? Radius { get; set; }

        public string Subject { get; set; }

        public string Level { get; set; }

        public int? CoachId { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        [JsonIgnore]
        public int StudentId { get; set; }
    }
}
