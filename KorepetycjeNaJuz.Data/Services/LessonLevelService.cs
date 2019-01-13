using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Exceptions;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Infrastructure.Services
{
	public class LessonLevelService : ILessonLevelService
	{
		private readonly ILessonRepository _lessonRepository;
		private readonly ILessonLevelRepository _lessonLevelRepository;
		private readonly IMapper _mapper;

		public LessonLevelService(
		    ILessonRepository lessonRepository,
		    ILessonLevelRepository lessonLevelRepository,
		    IMapper mapper)
		{
			this._lessonRepository = lessonRepository;
			this._lessonLevelRepository = lessonLevelRepository;
			this._mapper = mapper;
		}

		public async Task<LessonLevelDTO> CreateAsync(LessonLevelCreateDTO create)
		{
			LessonLevel level = _mapper.Map<LessonLevel>(create);

			level = await _lessonLevelRepository.AddAsync(level);

			return _mapper.Map<LessonLevelDTO>(level);
		}

		public async Task<LessonLevelDTO> DeleteAsync(int id)
		{
			var level = await _lessonLevelRepository.GetByIdAsync(id);

			if (level == null)
				throw new IdDoesNotExistException();

			await _lessonLevelRepository.DeleteAsync(id);

			return _mapper.Map<LessonLevelDTO>(level);
		}

		public async Task<IEnumerable<LessonLevelDTO>> GetAllAsync()
		{
			var levels = await _lessonLevelRepository.ListAllAsync();

			return _mapper.Map<IEnumerable<LessonLevelDTO>>(levels);
		}

		public async Task<LessonLevelDTO> GetAsync(int id)
		{
			var level = await _lessonLevelRepository.GetByIdAsync(id);

			if (level == null)
				throw new IdDoesNotExistException();

			return _mapper.Map<LessonLevelDTO>(level);
		}

		public async Task<IEnumerable<LessonLevelDTO>> GetByFilterAsync(LessonLevelFilterDTO filter)
		{
			if (string.IsNullOrEmpty(filter.LevelName))
				return new List<LessonLevelDTO>();

			var levels = await _lessonLevelRepository.FindByAsync(p => p.LevelName.StartsWith(filter.LevelName, System.StringComparison.OrdinalIgnoreCase));

			return _mapper.Map<IEnumerable<LessonLevelDTO>>(levels);
		}
	}
}
