using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Enums;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace KorepetycjeNaJuz.Infrastructure.Services
{
    public class CoachLessonService : ICoachLessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICoachLessonRepository _coachLessonRepository;
        private readonly IMapper _mapper;

        public CoachLessonService(
            ICoachLessonRepository coachLessonRepository,
            ILessonRepository lessonRepository,
            IMapper mapper)
        {
            this._coachLessonRepository = coachLessonRepository;
            this._lessonRepository = lessonRepository;
            this._mapper = mapper;
        }

        public bool IsUserAlreadySignedUpForCoachLesson(int coachLessonId, int userId)
        {
            var lesson = _lessonRepository.Query()
                                          .Where(x =>
                                            x.CoachLessonId == coachLessonId && 
                                            x.StudentId == userId)
                                          .FirstOrDefault();
            return lesson != null;
        }

        public bool IsCoachLessonAvailable(int coachLessonId)
        {
            var coachLesson = _coachLessonRepository.GetById(coachLessonId);
            var coachLessonStatus = (LessonStatuses)coachLesson.LessonStatusId;

            return coachLessonStatus == LessonStatuses.WaitingForStudents ||
                   coachLessonStatus == LessonStatuses.Reserved;
        }

        public bool IsCoachLessonExists(int coachLessonId)
        {
            return _coachLessonRepository.GetById(coachLessonId) != null;
        }

        public bool IsUserOwnerOfCoachLesson(int coachLessonId, int userId)
        {
            var coachLesson = _coachLessonRepository.GetById(coachLessonId);
            return coachLesson.CoachId == userId;
;       }
        public CoachLessonDTO MapCoachLessonDTO(CoachLesson coachLesson)
        {
            return _mapper.Map<CoachLessonDTO>(coachLesson);
        }
        public IEnumerable<CoachLessonDTO> MapCoachLessonsDTO(IEnumerable<CoachLesson> coachLessons)
        {
            return _mapper.Map<IEnumerable<CoachLessonDTO>>(coachLessons);
        }

    }
}
