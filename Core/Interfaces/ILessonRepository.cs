using KorepetycjeNaJuz.Core.Models;
using System.Collections.Generic;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface ILessonRepository : IRepositoryWithTypedId<Lesson, int>
    {
        List<Lesson> GetLessonsForCoachLesson(int coachLessonId);
    }
}
