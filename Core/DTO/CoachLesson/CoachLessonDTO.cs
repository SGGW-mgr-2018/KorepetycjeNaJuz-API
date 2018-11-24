using KorepetycjeNaJuz.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class CoachLessonDTO
    {
        public int Id { get; set; }
        public int CoachId { get; set; }

        public string CoachFirstName { get; set; }

        public string CoachLastName { get; set; }

        public string LessonStatusName { get; set; }

        public virtual ICollection<CoachLessonLevel> LessonLevels { get; set; }

        public int LessonSubjectId { get; set; }

        public string LessonSubject { get; set; }

        public decimal RatePerHour { get; set; }
        
        public DateTime DateStart { get; set; }
        
        public DateTime DateEnd { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

    }
}
