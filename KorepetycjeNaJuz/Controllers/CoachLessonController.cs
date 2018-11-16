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
            ICoachLessonService coachLessonService)
        {
            this._lessonService = lessonService;
            this._coachLessonService = coachLessonService;
            this._logger = LogManager.GetLogger("apiLogger");
        }


        /// <summary>
        /// metoda wyszukująca lekcje.
        /// </summary>
        /// <param name="getCoachLessonsByFiltersDTO">Dane lekcji</param>
        /// <returns>Wygenerowany token JWT</returns>
        /// <response code="200">Lista dostępnych lekcji</response>
        [HttpPost,Route("GetCoachLessonsByFilters"), AllowAnonymous]
        public IActionResult GetCoachLessonsByFilters([Required] GetCoachLessonsByFiltersDTO getCoachLessonsByFiltersDTO)
        {

            return new JsonResult("");
        }

    }
}
