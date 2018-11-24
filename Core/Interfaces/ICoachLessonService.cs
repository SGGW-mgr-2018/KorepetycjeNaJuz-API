using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Models;
using System.Collections.Generic;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface ICoachLessonService
    {
        bool IsCoachLessonExists(int coachLessonId);
        bool IsCoachLessonAvailable(int coachLessonId);
        bool IsUserAlreadySignedUpForCoachLesson(int coachLessonId, int userId);
        bool IsUserOwnerOfCoachLesson(int coachLessonId, int userId);
        CoachLessonDTO MapCoachLessonDTO(CoachLesson coachLesson);
        IEnumerable<CoachLessonDTO> MapCoachLessonsDTO(IEnumerable<CoachLesson> coachLessons);
    }
}
