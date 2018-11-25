using KorepetycjeNaJuz.Core.DTO;
using System.Collections.Generic;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface ICoachLessonService
    {
        bool IsCoachLessonExists(int coachLessonId);
        bool IsCoachLessonAvailable(int coachLessonId);
        bool IsUserAlreadySignedUpForCoachLesson(int coachLessonId, int userId);
        bool IsUserOwnerOfCoachLesson(int coachLessonId, int userId);
        IEnumerable<CoachLessonDTO> GetCoachLessonsByFilters(CoachLessonsByFiltersDTO filters);
    }
}
