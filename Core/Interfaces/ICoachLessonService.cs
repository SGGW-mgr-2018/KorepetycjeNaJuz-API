using KorepetycjeNaJuz.Core.Models;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface ICoachLessonService
    {
        bool IsCoachLessonExists(int coachLessonId);
        bool IsCoachLessonAvailable(int coachLessonId);
        bool IsUserAlreadySignedUpForCoachLesson(int coachLessonId, int userId);
        bool IsUserOwnerOfCoachLesson(int coachLessonId, int userId);
        CoachLesson GetById(int id);
    }
}
