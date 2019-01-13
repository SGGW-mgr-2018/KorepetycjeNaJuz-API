using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KorepetycjeNaJuz.Infrastructure;
using KorepetycjeNaJuz.Core.DTO;
using NLog;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace KorepetycjeNaJuz.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LessonLevelsController : ApiController
	{
		private readonly KorepetycjeContext ctx;
		private readonly ILogger logger;
		private readonly ILessonLevelService levelService;

		public LessonLevelsController(KorepetycjeContext context, ILessonLevelService lessonLevelService)
		{
			ctx = context;
			logger = LogManager.GetLogger("apiLogger");
			levelService = lessonLevelService;
		}

		/// <summary>
		/// Tworzy nowy poziom lekcji.
		/// </summary>
		/// <param name="create">Parametry nowego poziomu lekcji.</param>
		/// <returns>Nowy poziom lekcji.</returns>
		/// <response code="201">Poprawnie utworzono nowy poziom lekcji.</response>
		/// <response code="400">Przekazano niepoprawne zapytanie.</response>
		/// <response code="500">Błąd wewnętrzny.</response>
		[ProducesResponseType(typeof(LessonLevelDTO), 201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[HttpPost]
		[Route("Create")]
#if !DEBUG
		[Authorize("Bearer")]
#endif
		public async Task<IActionResult> Create([FromBody] LessonLevelCreateDTO create)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var level = await levelService.CreateAsync(create);

				return CreatedAtRoute("GetLessonLevel", new { id = level.Id }, level);
			}
			catch (Exception e)
			{
				logger.Fatal(e, $"POST LessonLevels/{nameof(Create)} {create}");

				return InternalServerError();
			}
		}

		/// <summary>
		/// Usuwa poziom lekcji.
		/// </summary>
		/// <param name="id">Identyfikator poziomu lekcji.</param>
		/// <returns>Usunięty poziom lekcji.</returns>
		/// <response code="200">Poprawnie usunięto poziom lekcji.</response>
		/// <response code="400">Przekazano niepoprawne zapytanie.</response>
		/// <response code="404">Poziom lekcji o podanym identyfikatorze nie istnieje.</response>
		/// <response code="500">Błąd wewnętrzny.</response>
		[ProducesResponseType(typeof(LessonLevelDTO), 200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpDelete]
		[Route("Delete/{id}")]
#if !DEBUG
		[Authorize("Bearer")]
#endif
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var level = await levelService.DeleteAsync(id);

				return Ok(level);
			}
			catch (IdDoesNotExistException)
			{
				return NotFound();
			}
			catch (Exception e)
			{
				logger.Fatal(e, $"DELETE LessonLevels/{id}");

				return InternalServerError();
			}
		}

		/// <summary>
		/// Pobiera poziom lekcji.
		/// </summary>
		/// <param name="id">Identyfikator poziomu lekcji.</param>
		/// <returns>Kod błędu.</returns>
		/// <response code="200">Poprawnie pobrano poziom lekcji.</response>
		/// <response code="400">Przekazano niepoprawne zapytanie.</response>
		/// <response code="404">Poziom lekcji o podanym identyfikatorze nie istnieje.</response>
		/// <response code="500">Błąd wewnętrzny.</response>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpGet]
		[Route("Get/{id}", Name = "GetLessonLevel")]
		public async Task<IActionResult> Get([FromRoute] int id)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var level = await levelService.GetAsync(id);

				return Ok(level);
			}
			catch (IdDoesNotExistException)
			{
				return NotFound();
			}
			catch (Exception e)
			{
				logger.Fatal(e, $"GET LessonLevels/{id}");

				return InternalServerError();
			}
		}

		/// <summary>
		/// Pobiera wszystkie poziomy lekcji.
		/// </summary>
		/// <returns>Wszystkie tematy lekcji.</returns>
		/// <response code="200">Poprawnie pobrano wszystkie poziomy lekcji.</response>
		/// <response code="400">Przekazano niepoprawne zapytanie.</response>
		/// <response code="500">Błąd wewnętrzny.</response>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var levels = await levelService.GetAllAsync();

				return Ok(levels);
			}
			catch (Exception e)
			{
				logger.Fatal(e, $"GET LessonLevels/{nameof(GetAll)}");

				return InternalServerError();
			}
		}

		/// <summary>
		/// Pobiera poziom lekcji z wykorzystaniem filtra.
		/// </summary>
		/// <param name="filter">Filtr.</param>
		/// <returns>Pobrane poziomy lekcji.</returns>
		/// <response code="200">Poprawnie pobrano poziom lekcji.</response>
		/// <response code="400">Przekazano niepoprawne zapytanie.</response>
		/// <response code="500">Błąd wewnętrzny.</response>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[HttpPost]
		[Route("GetByFilter")]
		public async Task<IActionResult> GetByFilter(LessonLevelFilterDTO filter)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var levels = await levelService.GetByFilterAsync(filter);

				return Ok(levels);
			}
			catch (Exception e)
			{
				logger.Fatal(e, $"POST LessonLevels/{nameof(GetByFilter)}");

				return InternalServerError();
			}
		}
	}
}