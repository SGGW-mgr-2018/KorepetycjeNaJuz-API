using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using NLog;
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
        /// <param name="model">Dane lekcji</param>
        /// <returns>Wygenerowany token JWT</returns>
        /// <response code="200">Lista dostępnych lekcji</response>
        /// <response code="404">Nie znaleziono lekcji o podanych kryteriach</response>
        [ProducesResponseType(typeof(CoachLesson), 200), ProducesResponseType(404)]
        [HttpPost, Route("GetCoachLessonsByFilters"), AllowAnonymous]
        public IActionResult GetCoachLessonsByFilters([Required] CoachLessonsByFiltersDTO model)
        {
            List<CoachLesson> output;

            if (model.DateFrom == null)
                model.DateFrom = DateTime.Now;

            if (model.DateTo == null)
                model.DateTo = model.DateFrom.Value.AddDays(1);

            IQueryable<CoachLesson> query = _coachLessonRepository.Query().Where(x => 
                (x.LessonStatus.Id == (int)LessonStatuses.WaitingForStudents || 
                 x.LessonStatus.Id == (int)LessonStatuses.Reserved) &&
                x.DateStart >= model.DateFrom && 
                x.DateEnd <= model.DateTo);

            // po tytule
            if (model.SubjectId != null)
                query = query.Where(x => x.Subject.Id == model.SubjectId.Value);

            // po poziomie
            if (model.LevelId != null)
                query = query.Where(x => x.LessonLevels.Any(i => i.LessonLevel.Id == model.LevelId.Value));

            // po coachID
            if (model.CoachId != null)
                query = query.Where(x => x.CoachId == model.CoachId.Value);

            if (model.Latitiude == null || model.Longitiude == null || model.Radius == null)
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
                    var distance = Distance(coachLesson.Address.Latitude, coachLesson.Address.Longitude, model.Latitiude.Value, model.Longitiude.Value);
                    if (distance <= model.Radius)
                    {
                        output.Add(coachLesson);
                    }
                }
            }

            return output.Any() ? StatusCode(200, output) : NotFound("Lessons not found.");
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
