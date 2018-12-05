using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Enums;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Infrastructure.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICoachLessonRepository _coachLessonRepository;
        private readonly IMapper _mapper;

        public LessonService(
            ILessonRepository lessonRepository,
            ICoachLessonRepository coachLessonRepository,
            IMapper mapper)
        {
            this._lessonRepository = lessonRepository;
            this._coachLessonRepository = coachLessonRepository;
            this._mapper = mapper;
        }

        public async Task CreateLessonAsync(LessonCreateDTO lessonCreateDTO)
        {
            var lesson = _mapper.Map<Lesson>(lessonCreateDTO);
            var coachLesson = await _coachLessonRepository.GetByIdAsync(lessonCreateDTO.CoachLessonId);

            lesson.Date = coachLesson.DateStart;
            lesson.NumberOfHours = (float)(coachLesson.DateEnd - coachLesson.DateStart).TotalHours;
            lesson.LessonStatusId = (int)LessonStatuses.Reserved;
            await _lessonRepository.AddAsync(lesson);

            coachLesson.LessonStatusId = (int)LessonStatuses.Reserved;
            await _coachLessonRepository.UpdateAsync(coachLesson);
        }

        public bool IsLessonExists(int lessonId)
        {
            return _lessonRepository.GetById(lessonId) != null;
        }

        public void RejectLesson(int id)
        {
            var lesson = _lessonRepository.GetById(id);

            lesson.LessonStatusId = (int)LessonStatuses.Rejected;
            _lessonRepository.UpdateAsync(lesson);
        }

        public Lesson GetById(int id)
        {
            return _lessonRepository.GetById(id);
        }

        public void ApproveLesson(int id)
        {
            var lesson = _lessonRepository.GetById(id);
            lesson.LessonStatusId = (int)LessonStatuses.Approved;
            lesson.CoachLesson.LessonStatusId = (int)LessonStatuses.Approved;

            _lessonRepository.Update(lesson);
        }

        public IEnumerable<LessonDTO> GetLessonsForCoachLesson(int coachLessonId)
        {
            var coachLesson = _coachLessonRepository.GetById(coachLessonId);

            return _mapper.Map<IEnumerable<LessonDTO>>(coachLesson.Lessons);
        }
    }
}
