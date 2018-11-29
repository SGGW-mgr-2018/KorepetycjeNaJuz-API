using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Exceptions;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Infrastructure.Services
{
	public class LessonSubjectService : ILessonSubjectService
	{
		private readonly ILessonRepository _lessonRepository;
		private readonly ILessonSubjectRepository _lessonSubjectRepository;
		private readonly IMapper _mapper;

		public LessonSubjectService(
		    ILessonRepository lessonRepository,
		    ILessonSubjectRepository lessonSubjectRepository,
		    IMapper mapper)
		{
			this._lessonRepository = lessonRepository;
			this._lessonSubjectRepository = lessonSubjectRepository;
			this._mapper = mapper;
		}

		public async Task<LessonSubjectDTO> CreateAsync(LessonSubjectCreateDTO create)
		{
			LessonSubject subject = _mapper.Map<LessonSubject>(create);

			subject = await _lessonSubjectRepository.AddAsync(subject);

			return _mapper.Map<LessonSubjectDTO>(subject);
		}

		public async Task<LessonSubjectDTO> DeleteAsync(int id)
		{
			var subject = await _lessonSubjectRepository.GetByIdAsync(id);

			if (subject == null)
				throw new IdDoesNotExistException();

			await _lessonSubjectRepository.DeleteAsync(id);

			return _mapper.Map<LessonSubjectDTO>(subject);
		}

		public async Task<IEnumerable<LessonSubjectDTO>> GetAllAsync()
		{
			var subjects = await _lessonSubjectRepository.ListAllAsync();

			return _mapper.Map<IEnumerable<LessonSubjectDTO>>(subjects);
		}

		public async Task<LessonSubjectDTO> GetAsync(int id)
		{
			var subject = await _lessonSubjectRepository.GetByIdAsync(id);

			if (subject == null)
				throw new IdDoesNotExistException();

			return _mapper.Map<LessonSubjectDTO>(subject);
		}

		public async Task<IEnumerable<LessonSubjectDTO>> GetByFilterAsync(LessonSubjectFilterDTO filter)
		{
			if (string.IsNullOrEmpty(filter.Name))
				return new List<LessonSubjectDTO>();

			var subjects = await _lessonSubjectRepository.FindByAsync(p => p.Name.StartsWith(filter.Name, System.StringComparison.OrdinalIgnoreCase));

			return _mapper.Map<IEnumerable<LessonSubjectDTO>>(subjects);
		}
	}
}
