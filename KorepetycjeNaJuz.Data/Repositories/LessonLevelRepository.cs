using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KorepetycjeNaJuz.Infrastructure.Repositories
{
    public class LessonLevelRepository : GenericRepository<LessonLevel>, ILessonLevelRepository
    {
        public LessonLevelRepository(KorepetycjeContext context)
            : base(context)
        {

        }
    }
}
