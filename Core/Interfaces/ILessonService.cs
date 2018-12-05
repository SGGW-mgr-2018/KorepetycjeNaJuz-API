using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Models;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface ILessonService
    {
        Task CreateLessonAsync(LessonCreateDTO lessonCreateDTO);
        bool IsLessonExists(int lessonId);
        Lesson GetById(int lessonId);
        void RejectLesson(int lessonId);
        void ApproveLesson(int lessonId);
    }
}
