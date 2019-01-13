using KorepetycjeNaJuz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Core.Interfaces
{
	public interface ILessonLevelRepository : IRepositoryWithTypedId<LessonLevel, int>
	{
		Task<IEnumerable<LessonLevel>> FindByAsync(Expression<Func<LessonLevel, bool>> predicate);
	}
}
