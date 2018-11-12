using KorepetycjeNaJuz.Core.Interfaces;

namespace KorepetycjeNaJuz.Infrastructure.Services
{
    public class CoachLessonService : ICoachLessonService
    {
        private readonly ICoachLessonRepository _coachLessonRepository;

        public CoachLessonService(ICoachLessonRepository coachLessonRepository)
        {
            this._coachLessonRepository = coachLessonRepository;
        }

        public bool IsCoachLessonExists(int coachLessonId)
        {
            return _coachLessonRepository.GetById(coachLessonId) != null;
        }
    }
}
