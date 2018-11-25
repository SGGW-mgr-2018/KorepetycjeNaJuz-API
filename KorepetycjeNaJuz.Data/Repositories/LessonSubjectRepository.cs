using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Core.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KorepetycjeNaJuz.Infrastructure.Repositories
{
	public class LessonSubjectRepository : GenericRepository<LessonSubject>, ILessonSubjectRepository
	{
		public LessonSubjectRepository(KorepetycjeContext ctx) : base(ctx)
		{
		}

		public virtual async Task<IEnumerable<LessonSubject>> FindByAsync(Expression<Func<LessonSubject, bool>> predicate)
		{
			return await _dbSet.Where(predicate).ToListAsync();
		}
	}
}
