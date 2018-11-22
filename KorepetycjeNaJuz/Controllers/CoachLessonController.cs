using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using NLog;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using KorepetycjeNaJuz.Core.Models;
using System.Linq;
using KorepetycjeNaJuz.Core.Enums;

namespace KorepetycjeNaJuz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachLessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly ICoachLessonService _coachLessonService;
        private readonly ICoachLessonRepository _coachLessonRepository;
        private readonly ILogger _logger;

        public CoachLessonController(
            ILessonService lessonService, 
            ICoachLessonService coachLessonService,
            ICoachLessonRepository coachLessonRepository)
        {
            this._lessonService = lessonService;
            this._coachLessonService = coachLessonService;
            this._coachLessonRepository = coachLessonRepository;
            this._logger = LogManager.GetLogger("apiLogger");
        }


        /// <summary>
        /// Metoda wyszukująca lekcje po zadanych filtrach.
        /// </summary>
        /// <param name="coachLessonsByFiltersDTO">Dane lekcji</param>
        /// <returns>Wygenerowany token JWT</returns>
        /// <response code="200">Lista dostępnych lekcji</response>
        /// <response code="404">Nie znaleziono lekcji o podanych kryteriach</response>
        [ProducesResponseType(typeof(CoachLesson), 200), ProducesResponseType(404)]
        [HttpPost, Route("GetCoachLessonsByFilters"), AllowAnonymous]
        public IActionResult GetCoachLessonsByFilters([Required] CoachLessonsByFiltersDTO coachLessonsByFiltersDTO)
        {
            List<CoachLesson> output;

            if (coachLessonsByFiltersDTO.DateFrom == null)
                coachLessonsByFiltersDTO.DateFrom = DateTime.Now;

            if (coachLessonsByFiltersDTO.DateTo == null)
                coachLessonsByFiltersDTO.DateTo = coachLessonsByFiltersDTO.DateFrom.AddDays(1);

            IQueryable<CoachLesson> query = _coachLessonRepository.Query().Where(
                x => x.LessonStatus.Id == (int)LessonStatuses.WaitingForStudents ||
                x.LessonStatus.Id == (int)LessonStatuses.Reserved &&
                x.DateStart >= coachLessonsByFiltersDTO.DateFrom &&
                x.DateEnd <= coachLessonsByFiltersDTO.DateTo);

            // po tytule
            if (coachLessonsByFiltersDTO.Subject != null)
                query = query.Where(x => x.Subject.Name.Equals(coachLessonsByFiltersDTO.Subject, StringComparison.InvariantCultureIgnoreCase));

            // po poziomie
            if (coachLessonsByFiltersDTO.Level != null)
                query = query.Where(x => x.LessonLevels.Any(i => i.LessonLevel.LevelName.Equals(coachLessonsByFiltersDTO.Level, StringComparison.CurrentCultureIgnoreCase)));

            // po coachID
            if (coachLessonsByFiltersDTO.CoachId != null)
                query = query.Where(x => x.CoachId == coachLessonsByFiltersDTO.CoachId.Value);

            if (coachLessonsByFiltersDTO.Latitiude == null || coachLessonsByFiltersDTO.Longitiude == null || coachLessonsByFiltersDTO.Radius == null)
            {
                // dodajemy wszystkie
                output = query.ToList();
            }
            else
            {
                output = new List<CoachLesson>();
                foreach (CoachLesson coachLesson in query)
                {
                    // po odległości
                    if (Distance(coachLesson.Address.Latitude, coachLesson.Address.Longitude, coachLessonsByFiltersDTO.Latitiude.GetValueOrDefault(), coachLessonsByFiltersDTO.Longitiude.GetValueOrDefault()) <= coachLessonsByFiltersDTO.Radius)
                    {
                        output.Add(coachLesson);
                    }
                }
            }

            return output.Count < 1 ? StatusCode(404, "Lessons not found.") : StatusCode(200, output);
        }

        private double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(Deg2rad(lat1)) * Math.Sin(Deg2rad(lat2)) + Math.Cos(Deg2rad(lat1)) * Math.Cos(Deg2rad(lat2)) * Math.Cos(Deg2rad(theta));
            dist = Math.Acos(dist);
            dist = Rad2deg(dist);
            dist = dist * 60 * 1.1515;
            dist = dist * 1.609344; // to kilomiters
            return (dist);
        }

        private double Deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double Rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
