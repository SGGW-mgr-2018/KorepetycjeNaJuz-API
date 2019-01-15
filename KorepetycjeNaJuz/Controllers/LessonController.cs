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
using KorepetycjeNaJuz.Core.Enums;
using KorepetycjeNaJuz.Core.Models;
using System.Collections.Generic;

namespace KorepetycjeNaJuz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly ICoachLessonService _coachLessonService;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public LessonController(
            ILessonService lessonService, 
            ICoachLessonService coachLessonService,
            IUserService userService)
        {
            this._lessonService = lessonService;
            this._coachLessonService = coachLessonService;
            this._userService = userService;
            this._logger = LogManager.GetLogger("apiLogger");
        }

        /// <summary>
        /// Zapisuje zalogowanego użytkownika na zadaną lekcję
        /// </summary>
        /// <param name="lessonCreateDTO"></param>
        /// <returns></returns>
        /// <response code="201">Poprawnie zapisano na lekcję</response>
        /// <response code="400">Niepoprawne dane</response>
        /// <response code="401">Wymagana autoryzacja</response>
        /// <response code="404">Podana lekcja nie istnieje</response>
        [ProducesResponseType(201), ProducesResponseType(400), ProducesResponseType(404), ProducesResponseType(401)]
        [HttpPost, Route("Create"), Authorize("Bearer")]
        public async Task<IActionResult> CreateLesson([Required, FromBody] LessonCreateDTO lessonCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var currentUserId = this.User.GetUserId().Value;
            var coachLessonId = lessonCreateDTO.CoachLessonId;
            lessonCreateDTO.StudentId = currentUserId;
            
            // Custom Validation 
            if (!_coachLessonService.IsCoachLessonExists(coachLessonId))
            {
                return NotFound();
            }
            if (_coachLessonService.IsUserOwnerOfCoachLesson(coachLessonId, currentUserId))
            {
                ModelState.AddModelError("CoachLessonId", "Nie można zapisać się na swoją lekcję.");
                return BadRequest(ModelState);
            }
            if (!_coachLessonService.IsCoachLessonAvailable(coachLessonId))
            {
                ModelState.AddModelError("CoachLessonId", "Nie można zapisać się tę lekcję.");
                return BadRequest(ModelState);
            }
            if (_coachLessonService.IsUserAlreadySignedUpForCoachLesson(coachLessonId, currentUserId))
            {
                ModelState.AddModelError("CoachLessonId", "Jesteś już zapisany na tę lekcję.");
                return BadRequest(ModelState);
            }

            try
            {
                await _lessonService.CreateLessonAsync(lessonCreateDTO);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during Lesson creation");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return StatusCode((int)HttpStatusCode.Created);
        }
        /// <summary>
        /// Ustawia status danej rezerwacji na "odrzucony" (rejected).
        /// </summary>
        /// <param name="id">ID lekcji</param>
        /// <returns></returns>
        /// <response code="200">Poprawnie odrzucono rezerwację</response>
        /// <response code="400">Niepoprawne dane</response>
        /// <response code="401">Wymagana autoryzacja</response>
        /// <response code="404">Podana rezerwacja nie istnieje</response>
        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(404), ProducesResponseType(401)]
        [HttpPost, Route("Reject"), Authorize("Bearer")]
        public async Task<IActionResult> RejectLesson([Required] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_lessonService.IsLessonExists(id))
                return NotFound();

            if (_lessonService.GetById(id).LessonStatus.Id == (int)LessonStatuses.Rejected)
            {
                ModelState.AddModelError("LessonStatus", "Rezerwacja została już odrzucona.");
                return BadRequest(ModelState);
            }

            try
            {
                await _lessonService.RejectLessonAsync(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during LessonStatus change");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return StatusCode((int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Ustawia status danej rezerwacji na "zatwierdzony" (approved), pozostałe odrzuca.
        /// </summary>
        /// <param name="id">ID lekcji</param>
        /// <returns></returns>
        /// <response code="200">Poprawnie zatwierdzono rezerwację</response>
        /// <response code="400">Niepoprawne dane</response>
        /// <response code="401">Wymagana autoryzacja</response>
        /// <response code="404">Podana rezerwacja nie istnieje</response>
        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(404), ProducesResponseType(401)]
        [HttpPost, Route("Approve"), Authorize("Bearer")]
        public async Task<IActionResult> ApproveLesson([Required] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_lessonService.IsLessonExists(id))
                return NotFound();

            if (_lessonService.GetById(id).LessonStatusId == (int)LessonStatuses.Approved)
            {
                ModelState.AddModelError("LessonStatus", "Rezerwacja została już zatwierdzona.");
                return BadRequest(ModelState);
            }
            if (_lessonService.GetById(id).CoachLesson.LessonStatusId == (int)LessonStatuses.Approved)
            {
                ModelState.AddModelError("LessonStatus", "Zajęcia mają już zatwierdzoną rezerwację.");
                return BadRequest(ModelState);
            }

            try
            {
                await _lessonService.ApproveLessonAsync(id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during LessonStatus change");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return StatusCode((int)HttpStatusCode.OK);
        }

        /// <summary>
        /// Pobiera lekcje dla danego ogłoszenia (coachLesson)
        /// </summary>
        /// <param name="coachLessonId">Id danego ogłoszenia</param>
        /// <returns></returns>
        /// <response code="200">Poprawnie pobrano lekcje dla ogłoszenia</response>
        /// <response code="400">Niepoprawne dane</response>
        /// <response code="401">Wymagana autoryzacja</response>
        /// <response code="403">Nie masz uprawnień, aby pobrać lekcje dla podanego ogłoszenia</response>
        /// <response code="404">Podane ogłoszenie nie istnieje</response>
        [ProducesResponseType(typeof(IEnumerable<LessonStudentDTO>), 200), ProducesResponseType(400)]
        [ProducesResponseType(401), ProducesResponseType(403), ProducesResponseType(404)]
        [HttpGet, Route("GetForCoachLesson/{coachLessonId}"), Authorize("Bearer")]
        public IActionResult GetLessonForCoachLesson([Required] int coachLessonId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_coachLessonService.IsCoachLessonExists(coachLessonId))
                return NotFound();

            var currentUserId = User.GetUserId().Value;
            if (!_coachLessonService.IsUserOwnerOfCoachLesson(coachLessonId, currentUserId))
                return Forbid();

            try
            {
                var lessons = _lessonService.GetLessonsForCoachLesson(coachLessonId);

                return Ok(lessons);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during LessonStatus change");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Umieszcza ocenę i opcjonalny komentarz do lekcji, jeśli zalogowany jest student ocena wystawiana jest korepetyście, jeśli korepetysta - uczniowi
        /// </summary>
        /// <param name="lessonRatingDTO"></param>
        /// <returns></returns>
        /// <response code="200">Poprawnie dodano ocenę i komentarz do lekcji</response>
        /// <response code="400">Niepoprawne dane</response>
        /// <response code="401">Wymagana autoryzacja</response>
        /// <response code="403">Nie masz uprawnień, aby dodać ocenę</response>
        /// <response code="404">Podana lekcja nie istnieje</response>
        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403), ProducesResponseType(404)]
        [HttpPost, Route("Rating/Post"), Authorize("Bearer")]
        public IActionResult PostLessonRating([Required, FromBody] LessonRatingDTO lessonRatingDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_lessonService.IsLessonExists(lessonRatingDTO.LessonId))
                return NotFound();


            var currentUserId = User.GetUserId().Value;
            Lesson lesson = _lessonService.GetById(lessonRatingDTO.LessonId);
            if (lesson.StudentId == currentUserId)
            {
                _lessonService.RateLessonCoach(lessonRatingDTO);
                _userService.CalculateCoachRating(lesson.CoachLesson.CoachId); // Aktualizacja pola CoachRating po nowej opinii
                return Ok();
            }
            else if (lesson.CoachLesson.CoachId == currentUserId)
            {
                _lessonService.RateLessonStudent(lessonRatingDTO);
                return Ok();
            }

            return Forbid();
        }

        /// <summary>
        /// Pobiera ocenę i komentarz o studencie dla danej lekcji
        /// </summary>
        /// <param name="id">Id lekcji</param>
        /// <returns></returns>
        /// <response code="200">Poprawnie pobrano ocenę i komentarz</response>
        /// <response code="400">Niepoprawne dane</response>
        /// <response code="401">Wymagana autoryzacja</response>
        /// <response code="404">Podana lekcja nie istnieje</response>
        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(404)]
        [HttpPost, Route("Rating/Student/{id}"), Authorize("Bearer")]
        public IActionResult GetLessonStudentRating([Required] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_lessonService.IsLessonExists(id))
                return NotFound();

            Lesson lesson = _lessonService.GetById(id);
            return Ok(new LessonRatingDTO(lesson.Id, (int)lesson.RatingOfStudent, lesson.OpinionOfStudent));
        }

        /// <summary>
        /// Pobiera ocenę i komentarz o korepetytorze dla danej lekcji
        /// </summary>
        /// <param name="id">Id lekcji</param>
        /// <returns></returns>
        /// <response code="200">Poprawnie pobrano ocenę i komentarz</response>
        /// <response code="400">Niepoprawne dane</response>
        /// <response code="401">Wymagana autoryzacja</response>
        /// <response code="404">Podana lekcja nie istnieje</response>
        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(404)]
        [HttpPost, Route("Rating/Coach/{id}"), Authorize("Bearer")]
        public IActionResult GetLessonCoachRating([Required] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_lessonService.IsLessonExists(id))
                return NotFound();

            Lesson lesson = _lessonService.GetById(id);
            return Ok(new LessonRatingDTO(lesson.Id, (int)lesson.RatingOfCoach, lesson.OpinionOfCoach));
        }
    }
}
