using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Enums;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;

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

        public void CreateLesson(LessonCreateDTO lessonCreateDTO)
        {
            var lesson = _mapper.Map<Lesson>(lessonCreateDTO);
            var coachLesson = _coachLessonRepository.GetById(lessonCreateDTO.CoachLessonId);

            lesson.Date = coachLesson.DateEnd;
            lesson.NumberOfHours = (coachLesson.DateEnd - coachLesson.DateStart).Hours;
            lesson.LessonStatusId = (int)LessonStatuses.WaitingToApprove;
            _lessonRepository.Add(lesson);

            coachLesson.LessonStatusId = (int)LessonStatuses.Reserved;
            _coachLessonRepository.Update(coachLesson);
        }

        public bool IsLessonExists(int lessonId)
        {
            return _lessonRepository.GetById(lessonId) != null;
        }
    }
}
