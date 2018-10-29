using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.DTO;
using KorepetycjeNaJuz.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Controllers
{
    [Route("api/Lesson")]
    public class LessonController : Controller
    {
        private readonly IRepositoryWithTypedId<Lesson, int> _repository;

        public LessonController(IRepositoryWithTypedId<Lesson, int> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("Add/{id}")]
        public IActionResult Add([FromBody] Lesson lesson)
        {
            var created = _repository.Add(lesson);
            return created.Id == 0 ? StatusCode(500) : (IActionResult)StatusCode(201, new LessonDTO() { Id = created.Id });
        }
        [HttpPost]
        [Route("AddAsync/{id}")]
        public async Task<IActionResult> AddAsync([FromBody] Lesson lesson)
        {
            var created = await _repository.AddAsync(lesson);
            return created.Id == 0 ? StatusCode(500) : (IActionResult)StatusCode(201, new LessonDTO() { Id = created.Id });
        }
        [HttpGet]
        [Route("ListAll")]
        public IActionResult ListAll()
        {
            var lessons = _repository.ListAll();
            return lessons.Count() < 1 ? StatusCode(404, "Lessons not found.") : StatusCode(200, lessons);
        }
        [HttpGet]
        [Route("ListAllAsync")]
        public IActionResult ListAllAsync()
        {
            var lessons = _repository.ListAllAsync();
            return lessons.Result.Count < 1 ? StatusCode(404, "Lessons not found.") : StatusCode(200, lessons);
        }

    }
}
