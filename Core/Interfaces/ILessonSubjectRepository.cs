using KorepetycjeNaJuz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Core.Interfaces
{
	public interface ILessonSubjectRepository : IRepositoryWithTypedId<LessonSubject, int>
	{
		Task<IEnumerable<LessonSubject>> FindByAsync(Expression<Func<LessonSubject, bool>> predicate);
	}
}
