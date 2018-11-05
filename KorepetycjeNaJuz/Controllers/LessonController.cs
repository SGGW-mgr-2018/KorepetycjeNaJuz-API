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
            _repository = repository as LessonRepository;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] Lesson lesson)
        {
            var created = _repository.Add(lesson);
            return created.Id == 0 ? StatusCode(500) : (IActionResult)StatusCode(201, new LessonDTO() { Id = created.Id });
        }

        [HttpPost]
        [Route("AddAsync")]
        public async Task<IActionResult> AddAsync([FromBody] Lesson lesson)
        {
            var created = await _repository.AddAsync(lesson);
            return created.Id == 0 ? StatusCode(500) : (IActionResult)StatusCode(201, new LessonDTO() { Id = created.Id });
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromBody] Lesson lesson)
        {
            _repository.Delete(lesson);
            return StatusCode(200);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _repository.Delete(id);
            return StatusCode(200);
        }

        [HttpDelete]
        [Route("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync([FromBody] Lesson lesson)
        {
            await _repository.DeleteAsync(lesson);
            return StatusCode(200);
        }

        [HttpDelete]
        [Route("DeleteAsync/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return StatusCode(200);
        }

        [HttpDelete]
        [Route("DeleteRange")]
        public IActionResult DeleteRange([FromBody] IEnumerable<Lesson> lessons)
        {
            _repository.DeleteRange(lessons);
            return StatusCode(200);
        }

        [HttpDelete]
        [Route("DeleteRangeAsync")]
        public async Task<IActionResult> DeleteRangeAsync([FromBody] IEnumerable<Lesson> lessons)
        {
            await _repository.DeleteRangeAsync(lessons);
            return StatusCode(200);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public IActionResult GetById(int id)
        {
            var lesson = _repository.GetById(id);
            return lesson == null ? StatusCode(500) : (IActionResult)StatusCode(200, lesson);
        }

        [HttpGet]
        [Route("GetAsync/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var lesson = await _repository.GetByIdAsync(id);
            return lesson == null ? StatusCode(500) : (IActionResult)StatusCode(200, lesson);
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
        public async Task<IActionResult> ListAllAsync()
        {
            var lessons = await _repository.ListAllAsync();
            return lessons.Count() < 1 ? StatusCode(404, "Lessons not found.") : StatusCode(200, lessons);
        }

        [HttpGet]
        [Route("Query")]
        public IActionResult Query()
        {
            var queryable = _repository.Query();
            return queryable.Count() < 1 ? StatusCode(404, "Lessons not found.") : StatusCode(200, queryable);
        }

        [HttpGet]
        [Route("LessionsByPosition/{latitiude}/{longitiude}/{radius}/{subjectId}/{levelId}")]
        public IActionResult LessionsByPosition(double latitiude, double longitiude, double radius, int subjectId, int levelId)
        {
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

            var query = _repository.ListAll();
            List<Lesson> output = new List<Lesson>();
            foreach (Lesson lesson in query)
            {
                if (    distance(lesson.CoachAddress.Latitude, lesson.CoachAddress.Longitude,latitiude,longitiude) <= radius && 
                        lesson.CoachLesson.LessonSubjectId == subjectId && 
                        lesson.CoachLesson.LessonLevelId == levelId)
                {
                    output.Add(lesson);
                }
            }
            return output.Count() < 1 ? StatusCode(404, "Lessons not found.") : StatusCode(200, output);
        }
        

        [HttpPost]
        [Route("Update")]
        public IActionResult Update([FromBody] Lesson lesson)
        {
            if (lesson.Id < 1)
                return StatusCode(500, "Provided ID is incorrect.");

            _repository.Update(lesson);
            return StatusCode(200, lesson);
        }

        [HttpPost]
        [Route("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync([FromBody] Lesson lesson)
        {
            if (lesson.Id < 1)
                return StatusCode(500, "Provided ID is incorrect.");

            await _repository.UpdateAsync(lesson);
            return StatusCode(200, lesson);
        }

    }
}
