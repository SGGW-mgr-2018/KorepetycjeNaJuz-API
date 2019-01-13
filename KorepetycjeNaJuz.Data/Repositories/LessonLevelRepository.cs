using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Infrastructure.Repositories
{
	public class LessonLevelRepository : GenericRepository<LessonLevel>, ILessonLevelRepository
	{
		public LessonLevelRepository(KorepetycjeContext ctx) : base(ctx)
		{
		}

		public virtual async Task<IEnumerable<LessonLevel>> FindByAsync(Expression<Func<LessonLevel, bool>> predicate)
		{
			return await _dbSet.Where(predicate).ToListAsync();
		}
	}
}
