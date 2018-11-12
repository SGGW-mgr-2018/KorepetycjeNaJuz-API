using KorepetycjeNaJuz.Core.DTO;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface ILessonService
    {
        void CreateLesson(LessonCreateDTO lessonCreateDTO);
        bool IsLessonExists(int lessonId);
    }
}
