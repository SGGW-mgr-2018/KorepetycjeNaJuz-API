using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;

namespace KorepetycjeNaJuz.Infrastructure.Repositories
{
    public class CoachLessonRepository : GenericRepository<CoachLesson>, ICoachLessonRepository
    {
        public CoachLessonRepository(KorepetycjeContext context)
            : base(context)
        {

        }
    }
}
