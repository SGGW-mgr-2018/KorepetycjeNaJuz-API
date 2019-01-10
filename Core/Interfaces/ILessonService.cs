using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface ILessonService
    {
        Task CreateLessonAsync(LessonCreateDTO lessonCreateDTO);
        bool IsLessonExists(int lessonId);
        Lesson GetById(int lessonId);
        Task RejectLessonAsync(int lessonId);
        Task ApproveLessonAsync(int lessonId);
        IEnumerable<LessonStudentDTO> GetLessonsForCoachLesson(int coachLessonId);
        void RateLessonStudent(LessonRatingDTO lessonRatingDTO);
        void RateLessonCoach(LessonRatingDTO lessonRatingDTO);
    }
}
