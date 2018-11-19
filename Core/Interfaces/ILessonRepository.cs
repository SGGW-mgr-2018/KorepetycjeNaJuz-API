using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Models;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface ILessonRepository : IRepositoryWithTypedId<Lesson, int>
    {
        Task CreateLessonAsync(LessonCreateDTO lessonCreateDTO);
        bool IsLessonExists(int id);
        void RejectLesson(int id);

    }
}
