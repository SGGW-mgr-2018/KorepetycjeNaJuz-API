using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Models;
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
        CoachLesson GetById(int id);
        IEnumerable<CoachLessonDTO> GetCoachLessonsByFilters(CoachLessonsByFiltersDTO filters);

        bool IsTimeAvailable(int coachID, DateTime startDate, DateTime endDate);
        Task CreateCoachLesson(CoachLessonCreateDTO coachLessonDTO, int currentUserID);
    }


}
