using System;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class CoachLessonsByFiltersDTO
    {
        /// <summary>
        /// Szerokość geograficzna 
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Długość geograficzna
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Promień okręgu wyszukiwania (w kilometrach)
        /// </summary>
        public double? Radius { get; set; }

        /// <summary>
        /// Id tematu/przedmiotu lekcji
        /// </summary>
        public int? SubjectId { get; set; }

        /// <summary>
        /// Id poziomu lekcji
        /// </summary>
        public int? LevelId { get; set; }

        /// <summary>
        /// Id korepetytora prowadzącego lekcje
        /// </summary>
        public int? CoachId { get; set; }

        /// <summary>
        /// Data rozpoczęcia lekcji
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Data zakończenia lekcji
        /// </summary>
        public DateTime? DateTo { get; set; }

        public int? StudentId { get; set; }
    }
}
