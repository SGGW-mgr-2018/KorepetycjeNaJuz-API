using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Models;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface ILessonService
    {
        Task CreateLessonAsync(LessonCreateDTO lessonCreateDTO);
        bool IsLessonExists(int lessonId);
        Lesson GetById(int id);
        void RejectLesson(int id);
        void ApproveLesson(LessonAcceptDTO lessonAcceptDTO);
    }
}
