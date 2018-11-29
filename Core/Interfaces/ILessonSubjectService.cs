using KorepetycjeNaJuz.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Core.Interfaces
{
	public interface ILessonSubjectService
	{
		Task<LessonSubjectDTO> CreateAsync(LessonSubjectCreateDTO create);
		Task<LessonSubjectDTO> DeleteAsync(int id);
		Task<LessonSubjectDTO> GetAsync(int id);
		Task<IEnumerable<LessonSubjectDTO>> GetAllAsync();
		Task<IEnumerable<LessonSubjectDTO>> GetByFilterAsync(LessonSubjectFilterDTO filter);
	}
}
