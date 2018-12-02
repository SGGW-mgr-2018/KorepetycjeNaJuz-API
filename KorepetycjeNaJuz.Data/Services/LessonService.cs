using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Enums;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
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

            lesson.Date = coachLesson.DateEnd;
            lesson.NumberOfHours = (coachLesson.DateEnd - coachLesson.DateStart).Hours;
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

            lesson.LessonStatus.Id = (int)LessonStatuses.Rejected;
            _lessonRepository.UpdateAsync(lesson);
        }
        public Lesson GetById(int id)
        {
            return _lessonRepository.GetById(id);
        }
        public void ApproveLesson(LessonAcceptDTO lessonAcceptDTO)
        {
            var selectedLesson = _lessonRepository.GetById(lessonAcceptDTO.LessonId);
            var coachLesson = _coachLessonRepository.GetById(lessonAcceptDTO.CoachLessonId);
            var allLessonsForCoachLesson = _lessonRepository.GetLessonsForCoachLesson(lessonAcceptDTO.CoachLessonId);

            foreach (var lesson in allLessonsForCoachLesson)
            {
                if (lesson.Id == lessonAcceptDTO.LessonId)
                    selectedLesson.LessonStatus.Id = (int)LessonStatuses.Approved;
                else
                    lesson.LessonStatusId = (int)LessonStatuses.Rejected;
                _lessonRepository.UpdateAsync(lesson);
            }

            coachLesson.LessonStatus.Id = (int)LessonStatuses.Approved;
            _coachLessonRepository.UpdateAsync(coachLesson);
            _lessonRepository.UpdateAsync(selectedLesson);

        }
    }
}
