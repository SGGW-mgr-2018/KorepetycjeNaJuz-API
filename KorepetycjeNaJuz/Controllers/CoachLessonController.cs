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
        /// <param name="getCoachLessonsByFiltersDTO">Dane lekcji</param>
        /// <returns>Wygenerowany token JWT</returns>
        /// <response code="200">Lista dostępnych lekcji</response>
        /// <response code="404">Nie znaleziono lekcji o podanych kryteriach</response>
        [HttpPost,Route("GetCoachLessonsByFilters"), AllowAnonymous]
        public IActionResult GetCoachLessonsByFilters([Required] GetCoachLessonsByFiltersDTO getCoachLessonsByFiltersDTO)
        {
            List<CoachLesson> output;


            if (getCoachLessonsByFiltersDTO.DateFrom == null)
                getCoachLessonsByFiltersDTO.DateFrom = DateTime.Now;

            if (getCoachLessonsByFiltersDTO.DateTo == null)
                getCoachLessonsByFiltersDTO.DateTo = getCoachLessonsByFiltersDTO.DateFrom.AddDays(1);

            IEnumerable<CoachLesson> query = _coachLessonRepository.ListAll().Where(
                x => x.LessonStatus.Id == (int)LessonStatuses.WaitingForStudents ||
                x.LessonStatus.Id == (int)LessonStatuses.Reserved &&
                x.DateStart >= getCoachLessonsByFiltersDTO.DateFrom &&
                x.DateEnd <= getCoachLessonsByFiltersDTO.DateTo);


            // po tytule
            if (getCoachLessonsByFiltersDTO.Subject != null)
                query.Where(x=> x.Subject.Name == getCoachLessonsByFiltersDTO.Subject );

            // po poziomie
            if (getCoachLessonsByFiltersDTO.Level != null)
                query.Where(x => x.LessonLevels.Any(i => i.LessonLevel.LevelName == getCoachLessonsByFiltersDTO.Level));

            // po coachID
            if (getCoachLessonsByFiltersDTO.CoachId != null)
                query.Where(x => x.CoachId == getCoachLessonsByFiltersDTO.CoachId);

            if (getCoachLessonsByFiltersDTO.Latitiude == null || getCoachLessonsByFiltersDTO.Longitiude == null || getCoachLessonsByFiltersDTO.Radius == null)
            {
                // dodajemy wszystkie
                output = query.ToList<CoachLesson>();
            }
            else
            {
                output = new List<CoachLesson>();
                foreach (CoachLesson coachLesson in query)
                {
                    // po odległości
                    if (distance(coachLesson.Address.Latitude, coachLesson.Address.Longitude, getCoachLessonsByFiltersDTO.Latitiude.GetValueOrDefault(), getCoachLessonsByFiltersDTO.Longitiude.GetValueOrDefault()) <= getCoachLessonsByFiltersDTO.Radius)
                    {
                        output.Add(coachLesson);
                    }
                }
            }

            

            return output.Count < 1 ? StatusCode(404, "Lessons not found.") : StatusCode(200, output);
        }
        double distance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            dist = dist * 1.609344; // to kilomiters
            return (dist);
        }
        double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

    }
}
