using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class CoachLessonCalendarFiltersDTO
    {
        /// <summary>
        /// Data początkowa
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Data końcowa
        /// </summary>
        public DateTime? DateTo { get; set; }
    }
}
