using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;

namespace KorepetycjeNaJuz.Infrastructure.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(KorepetycjeContext dbContext)
            : base(dbContext)
        {

        }
    }
}
