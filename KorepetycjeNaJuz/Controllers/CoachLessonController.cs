using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using KorepetycjeNaJuz.Core.Exceptions;
using KorepetycjeNaJuz.Core.Attributes.SwaggerAttributes;
using KorepetycjeNaJuz.Core.Helpers;

namespace KorepetycjeNaJuz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachLessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly ICoachLessonService _coachLessonService;
        private readonly ILessonSubjectService _lessonSubjectService;
        private readonly ILessonLevelRepository _lessonLevelRepository;
        private readonly ILogger _logger;

        public CoachLessonController(
            ILessonService lessonService,
            ICoachLessonService coachLessonService,
            ILessonSubjectService lessonSubjectService,
            ILessonLevelRepository lessonLevelRepository)
        {
            this._lessonService = lessonService;
            this._coachLessonService = coachLessonService;
            this._lessonSubjectService = lessonSubjectService;
            this._lessonLevelRepository = lessonLevelRepository;
            this._logger = LogManager.GetLogger("apiLogger");
        }

        /// <summary>
        /// Zwraca historię lekcji zalogowanego użytkownika
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<CoachLessonHistoryDTO>), 200)]
        [ProducesResponseType(400), ProducesResponseType(401)]
        [HttpGet, Route("History"), Authorize("Bearer")]
        public IActionResult History()
        {
            try
            {
                var currentUserId = User.GetUserId().Value;
                var output = _coachLessonService.GetCoachLessonsHistory(currentUserId);

                return Ok(output);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during CoachLessonController|History");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Zwraca listę lekcji na które zalogowany użytkownik jest zapisany (Uczeń) oraz
        /// lekcje które użytkownik wystawił (Korepetytor)
        /// </summary>
        /// <param name="coachLessonCalendarFiltersDTO"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<CoachLessonCalendarDTO>), 200)]
        [ProducesResponseType(400), ProducesResponseType(401)]
        [HttpGet, Route("Calendar"), Authorize("Bearer")]
        public IActionResult Calendar([FromQuery] CoachLessonCalendarFiltersDTO coachLessonCalendarFiltersDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var currentUserId = User.GetUserId().Value;
                var output = _coachLessonService.GetCoachLessonsCalendar(coachLessonCalendarFiltersDTO, currentUserId);

                return Ok(output);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during CoachLessonController|Calendar");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Metoda wyszukująca lekcje po zadanych filtrach.
        /// </summary>
        /// <param name="coachLessonsByFiltersDTO">Dane lekcji</param>        
        /// <returns></returns>
        /// <response code="200">Lista dostępnych lekcji</response>
        /// <response code="400">Błedne parametry</response>
        /// <response code="404">Nie znaleziono lekcji o podanych kryteriach</response>
        /// <response code="500">Błąd wewnętrzny</response>
        [ProducesResponseType(typeof(IEnumerable<CoachLessonDTO>), 200)]
        [ProducesResponseType(400), ProducesResponseType(404), ProducesResponseType(500)]
        [HttpGet, Route("CoachLessonsByFilters"), AllowAnonymous]        
        public IActionResult GetCoachLessonsByFilters([FromQuery, Required] CoachLessonsByFiltersDTO coachLessonsByFiltersDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var output = _coachLessonService.GetCoachLessonsByFilters(coachLessonsByFiltersDTO);

                return output.Any() ? StatusCode(200, output) : NotFound("Lessons not found.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during GetCoachLessonsByFilters");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Dodaje nowe ogłoszenie lekcji (coachLesson)
        /// </summary>
        /// <param name="coachLessonDTO">Dane lekcji</param>
        /// <returns></returns>
        /// <response code="201">Stworzono CoachLesson</response>
        /// <response code="400">Błedne parametry</response>
        /// <response code="409">Czas jest już zajęty</response>
        [ProducesResponseType(201), ProducesResponseType(400), ProducesResponseType(409)]
        [HttpPost, Route("Create"), Authorize("Bearer")]
        public async Task<IActionResult> CreateCoachLesson([Required, FromBody] CoachLessonCreateDTO coachLessonDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int currentUserID = this.User.GetUserId().Value;
            if (!_coachLessonService.IsTimeAvailable(currentUserID, coachLessonDTO.DateStart.Value, coachLessonDTO.DateEnd.Value))
            {
                return StatusCode((int)HttpStatusCode.Conflict);
            }

            TimeSpan span = coachLessonDTO.DateEnd.Value.Subtract(coachLessonDTO.DateStart.Value);
            if ((span.TotalMinutes % coachLessonDTO.Time != 0))
            {
                ModelState.AddModelError("Time", "Nie da się poprawnie rozłożyć lekcji w danym przedziale czasowym.");
                return BadRequest(ModelState);
            }

            try
            {
                await _lessonSubjectService.GetAsync(coachLessonDTO.LessonSubjectId);
            }
            catch (IdDoesNotExistException)
            {
                ModelState.AddModelError("LessonSubjectId", "Podany przedmiot lekcji nie istnieje.");
                return BadRequest(ModelState);
            }

            foreach (var levelId in coachLessonDTO.LessonLevels)
            {
                if (!_lessonLevelRepository.Query().Any(x => x.Id == levelId))
                {
                    ModelState.AddModelError("LessonLevels", "Przynajmniej jeden poziom nie istnieje.");
                    return BadRequest(ModelState);
                } 
            }

            await _coachLessonService.CreateCoachLesson(coachLessonDTO, currentUserID);

            return StatusCode((int)HttpStatusCode.Created);
        }

    }
}
