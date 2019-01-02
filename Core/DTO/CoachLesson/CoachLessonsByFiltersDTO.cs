using KorepetycjeNaJuz.Core.Attributes.SwaggerAttributes;
using System;

namespace KorepetycjeNaJuz.Core.DTO
{
    public class CoachLessonsByFiltersDTO
    {
        /// <summary>
        /// Szerokość geograficzna 
        /// </summary>                
        [SwaggerParameter(description: "Szerokość geograficzna")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Długość geograficzna
        /// </summary>
        [SwaggerParameter(description: "Długość geograficzna")]
        public double? Longitude { get; set; }

        /// <summary>
        /// Promień okręgu wyszukiwania (w kilometrach)
        /// </summary>
        [SwaggerParameter(description: "Promień okręgu wyszukiwania (w kilometrach)")]
        public double? Radius { get; set; }

        /// <summary>
        /// Id tematu/przedmiotu lekcji
        /// </summary>
        [SwaggerParameter(description: "Id tematu/przedmiotu lekcji")]
        public int? SubjectId { get; set; }

        /// <summary>
        /// Id poziomu lekcji
        /// </summary>
        [SwaggerParameter(description: "Id poziomu lekcji")]
        public int? LevelId { get; set; }

        /// <summary>
        /// Id korepetytora prowadzącego lekcje
        /// </summary>
        [SwaggerParameter(description: "Id korepetytora prowadzącego lekcje")]
        public int? CoachId { get; set; }

        /// <summary>
        /// Data rozpoczęcia lekcji
        /// </summary>
        [SwaggerParameter(description: "Data rozpoczęcia lekcji")]
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Data zakończenia lekcji
        /// </summary>
        [SwaggerParameter(description: "Data zakończenia lekcji")]
        public DateTime? DateTo { get; set; }

        public int? StudentId { get; set; }
    }
}
