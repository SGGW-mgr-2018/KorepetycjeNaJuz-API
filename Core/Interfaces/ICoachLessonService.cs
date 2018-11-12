using System;
using System.Collections.Generic;
using System.Text;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface ICoachLessonService
    {
        bool IsCoachLessonExists(int coachLessonId);
    }
}
