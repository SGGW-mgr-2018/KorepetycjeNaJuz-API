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

namespace KorepetycjeNaJuz.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly ICoachLessonService _coachLessonService;
        private readonly ILogger _logger;

        public LessonController(
            ILessonService lessonService, 
            ICoachLessonService coachLessonService)
        {
            this._lessonService = lessonService;
            this._coachLessonService = coachLessonService;
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
        public IActionResult RejectLesson([Required] int id)
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
                _lessonService.RejectLesson(id);
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
        /// <param name="lessonAcceptDTO">Zawiera ID zajęć oraz ID rezerwacji.</param>
        /// <returns></returns>
        /// <response code="200">Poprawnie zatwierdzono rezerwację</response>
        /// <response code="400">Niepoprawne dane</response>
        /// <response code="401">Wymagana autoryzacja</response>
        /// <response code="404">Podana rezerwacja nie istnieje</response>
        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(404), ProducesResponseType(401)]
        [HttpPost, Route("Approve"), Authorize("Bearer")]
        public IActionResult ApproveLesson([Required] LessonAcceptDTO lessonAcceptDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_lessonService.IsLessonExists(lessonAcceptDTO.LessonId))
                return NotFound();
            if (!_coachLessonService.IsCoachLessonExists(lessonAcceptDTO.CoachLessonId))
                return NotFound();

            if (_lessonService.GetById(lessonAcceptDTO.LessonId).LessonStatus.Id == (int)LessonStatuses.Approved)
            {
                ModelState.AddModelError("LessonStatus", "Rezerwacja została już zatwierdzona.");
                return BadRequest(ModelState);
            }
            if (_coachLessonService.GetById(lessonAcceptDTO.CoachLessonId).LessonStatus.Id == (int)LessonStatuses.Approved)
            {
                ModelState.AddModelError("LessonStatus", "Zajęcia mają już zatwierdzoną rezerwację.");
                return BadRequest(ModelState);
            }

            try
            {
                _lessonService.ApproveLesson(lessonAcceptDTO);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during LessonStatus change");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return StatusCode((int)HttpStatusCode.OK);
        }

    }
}
