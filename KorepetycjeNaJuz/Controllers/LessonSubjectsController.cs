using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KorepetycjeNaJuz.Infrastructure;
using KorepetycjeNaJuz.Core.DTO;
using NLog;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Exceptions;

namespace KorepetycjeNaJuz.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class LessonSubjectsController : ApiController
	{
		private readonly KorepetycjeContext ctx;
		private readonly ILogger logger;
		private readonly ILessonSubjectService subjectService;

		public LessonSubjectsController(KorepetycjeContext context, ILessonSubjectService lessonSubjectService)
		{
			ctx = context;
			logger = LogManager.GetLogger("apiLogger");
			subjectService = lessonSubjectService;
		}

		/// <summary>
		/// Tworzy nowy temat lekcji.
		/// </summary>
		/// <param name="create">Parametry nowego tematu lekcji.</param>
		/// <returns>Nowy temat lekcji.</returns>
		/// <response code="201">Poprawnie utworzono nowy temat lekcji.</response>
		/// <response code="400">Przekazano niepoprawne zapytanie.</response>
		/// <response code="500">Błąd wewnętrzny.</response>
		[ProducesResponseType(typeof(LessonSubjectDTO), 201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[HttpPost]
		[Route("Create")]
#if !DEBUG
		[Authorize("Bearer")]
#endif
		public async Task<IActionResult> Create([FromBody] LessonSubjectCreateDTO create)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var subject = await subjectService.CreateAsync(create);

				return CreatedAtRoute("Get", new { id = subject.Id }, subject);
			}
			catch (Exception e)
			{
				logger.Fatal(e, $"POST LessonSubjects/{nameof(Create)} {create}");

				return InternalServerError();
			}
		}

		/// <summary>
		/// Usuwa temat lekcji.
		/// </summary>
		/// <param name="id">Identyfikator tematu lekcji.</param>
		/// <returns>Usunięty temat lekcji.</returns>
		/// <response code="200">Poprawnie usunięto temat lekcji.</response>
		/// <response code="400">Przekazano niepoprawne zapytanie.</response>
		/// <response code="404">Temat lekcji o podanym identyfikatorze nie istnieje.</response>
		/// <response code="500">Błąd wewnętrzny.</response>
		[ProducesResponseType(typeof(LessonSubjectDTO), 200)]
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

				var subject = await subjectService.DeleteAsync(id);

				return Ok(subject);
			}
			catch (IdDoesNotExistException)
			{
				return NotFound();
			}
			catch (Exception e)
			{
				logger.Fatal(e, $"DELETE LessonSubjects/{id}");

				return InternalServerError();
			}
		}

		/// <summary>
		/// Pobiera temat lekcji.
		/// </summary>
		/// <param name="id">Identyfikator tematu lekcji.</param>
		/// <returns>Kod błędu.</returns>
		/// <response code="200">Poprawnie pobrano temat lekcji.</response>
		/// <response code="400">Przekazano niepoprawne zapytanie.</response>
		/// <response code="404">Temat lekcji o podanym identyfikatorze nie istnieje.</response>
		/// <response code="500">Błąd wewnętrzny.</response>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		[HttpGet]
		[Route("Get/{id}")]
		public async Task<IActionResult> Get([FromRoute] int id)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var subject = await subjectService.GetAsync(id);

				return Ok(subject);
			}
			catch (IdDoesNotExistException)
			{
				return NotFound();
			}
			catch (Exception e)
			{
				logger.Fatal(e, $"GET LessonSubjects/{id}");

				return InternalServerError();
			}
		}

		/// <summary>
		/// Pobiera wszystkie tematy lekcji.
		/// </summary>
		/// <returns>Wszystkie tematy lekcji.</returns>
		/// <response code="200">Poprawnie pobrano wszystkie tematy lekcji.</response>
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

				var subjects = await subjectService.GetAllAsync();

				return Ok(subjects);
			}
			catch (Exception e)
			{
				logger.Fatal(e, $"GET LessonSubjects/{nameof(GetAll)}");

				return InternalServerError();
			}
		}

		/// <summary>
		/// Pobiera tematy lekcji z wykorzystaniem filtra.
		/// </summary>
		/// <param name="filter">Filtr.</param>
		/// <returns>Pobrane tematy lekcji.</returns>
		/// <response code="200">Poprawnie pobrano tematy lekcji.</response>
		/// <response code="400">Przekazano niepoprawne zapytanie.</response>
		/// <response code="500">Błąd wewnętrzny.</response>
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		[HttpPost][Route("GetByFilter")]
		public async Task<IActionResult> GetByFilter(LessonSubjectFilterDTO filter)
		{
			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var subjects = await subjectService.GetByFilterAsync(filter);

				return Ok(subjects);
			}
			catch (Exception e)
			{
				logger.Fatal(e, $"POST LessonSubjects/{nameof(GetByFilter)}");

				return InternalServerError();
			}
		}
	}
}