using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Models;

namespace KorepetycjeNaJuz.Core.Mappings
{
	public class LessonSubjectMappings : Profile
	{
		public LessonSubjectMappings()
		{
			CreateMap<LessonSubjectDTO, LessonSubject>();
			CreateMap<LessonSubject, LessonSubjectDTO>();
			CreateMap<LessonSubjectCreateDTO, LessonSubject>();
		}
	}
}
