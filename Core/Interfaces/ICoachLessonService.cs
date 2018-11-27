using KorepetycjeNaJuz.Core.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface ICoachLessonService
    {
        bool IsCoachLessonExists(int coachLessonId);
        bool IsCoachLessonAvailable(int coachLessonId);
        bool IsUserAlreadySignedUpForCoachLesson(int coachLessonId, int userId);
        bool IsUserOwnerOfCoachLesson(int coachLessonId, int userId);
        IEnumerable<CoachLessonDTO> GetCoachLessonsByFilters(CoachLessonsByFiltersDTO filters);

        bool IsTimeAvailable(int coachID, DateTime startDate, DateTime endDate);
        Task AddNewCoachLesson(CoachLessonCreateDTO coachLessonDTO);
    }


}
