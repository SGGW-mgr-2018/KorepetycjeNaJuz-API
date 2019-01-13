using KorepetycjeNaJuz.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Core.Interfaces
{
	public interface ILessonLevelService
	{
		Task<LessonLevelDTO> CreateAsync(LessonLevelCreateDTO create);
		Task<LessonLevelDTO> DeleteAsync(int id);
		Task<LessonLevelDTO> GetAsync(int id);
		Task<IEnumerable<LessonLevelDTO>> GetAllAsync();
		Task<IEnumerable<LessonLevelDTO>> GetByFilterAsync(LessonLevelFilterDTO filter);
	}
}
