using KorepetycjeNaJuz.Core.Enums;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System.Linq;

namespace KorepetycjeNaJuz.Infrastructure.Services
{
    public class CoachLessonService : ICoachLessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICoachLessonRepository _coachLessonRepository;

        public CoachLessonService(
            ICoachLessonRepository coachLessonRepository,
            ILessonRepository lessonRepository)
        {
            this._coachLessonRepository = coachLessonRepository;
            this._lessonRepository = lessonRepository;
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

        public CoachLesson GetById(int id)
        {
            return _coachLessonRepository.GetById(id);
        }
    }
}
