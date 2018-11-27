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
        /// Metoda wyszukująca lekcje po zadanych filtrach.
        /// </summary>
        /// <param name="filters">Dane lekcji</param>
        /// <returns></returns>
        /// <response code="200">Lista dostępnych lekcji</response>
        /// <response code="400">Błedne parametry</response>
        /// <response code="404">Nie znaleziono lekcji o podanych kryteriach</response>
        /// <response code="500">Błąd wewnętrzny</response>
        [ProducesResponseType(typeof(IEnumerable<CoachLessonDTO>), 200)]
        [ProducesResponseType(400), ProducesResponseType(404), ProducesResponseType(500)]
        [HttpGet, Route("CoachLessonsByFilters"), AllowAnonymous]
        public IActionResult GetCoachLessonsByFilters([FromQuery, Required] CoachLessonsByFiltersDTO filters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var output = _coachLessonService.GetCoachLessonsByFilters(filters);

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
        /// <param name="model">Dane lekcji</param>
        /// <returns></returns>
        /// <response code="201">Stworzono CoachLesson</response>
        /// <response code="400">Błedne parametry</response>
        /// <response code="409">Czas jest już zajęty</response>
        [ProducesResponseType(201), ProducesResponseType(400), ProducesResponseType(409)]
        [HttpPost, Route("Create"), Authorize("Bearer")]
        public async Task<IActionResult> AddNewCoachLesson([Required, FromBody] CoachLessonCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_coachLessonService.IsTimeAvailable(model.CoachId, model.DateStart.Value, model.DateEnd.Value))
            {
                return StatusCode((int)HttpStatusCode.Conflict);
            }

            TimeSpan span = model.DateEnd.Value.Subtract(model.DateStart.Value);
            if (((int)span.TotalMinutes) == model.Time)
            {
                ModelState.AddModelError("Time", "Zbyt długi czas lekcji, aby utworzyć lekcję w zadanym przedziale czasowym");
                return BadRequest(ModelState);
            }

            try
            {
                await _lessonSubjectService.GetAsync(model.LessonSubjectId);
            }
            catch (IdDoesNotExistException)
            {
                ModelState.AddModelError("LessonSubjectId", "Podany przedmiot lekcji nie istnieje.");
                return BadRequest(ModelState);
            }

            foreach (var levelId in model.LessonLevels)
            {
                if (!_lessonLevelRepository.Query().Any(x => x.Id == levelId))
                {
                    ModelState.AddModelError("LessonLevels", "Przynajmniej jeden poziom nie istnieje.");
                    return BadRequest(ModelState);
                } 
            }

            await _coachLessonService.AddNewCoachLesson(model);

            return StatusCode((int)HttpStatusCode.Created);
        }

    }
}
