using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using NLog;
using System.Net;

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

        [HttpPost, Route("add"), Authorize("Bearer")]
        public IActionResult CreateLesson([Required, FromBody] LessonCreateDTO lessonCreateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_coachLessonService.IsCoachLessonExists(lessonCreateDTO.CoachLessonId))
            {
                ModelState.AddModelError("CoachLessonId", "Lekcja na którą chcesz się zapisać nie istnieje.");
                return BadRequest(ModelState);
            }

            var currentUserId = this.User.GetUserId();
            lessonCreateDTO.StudentId = currentUserId.Value;

            if (currentUserId == lessonCreateDTO.CoachLessonId)
            {
                ModelState.AddModelError("CoachLessonId", "Nie można zapisać się na własną lekcję.");
                return BadRequest(ModelState);
            }

            try
            {
                _lessonService.CreateLesson(lessonCreateDTO);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error during Lesson creation");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}
