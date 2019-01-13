using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Models;

namespace KorepetycjeNaJuz.Core.Mappings
{
	public class LessonLevelMappings : Profile
	{
		public LessonLevelMappings()
		{
			CreateMap<LessonLevelDTO, LessonLevel>();
			CreateMap<LessonLevel, LessonLevelDTO>();
			CreateMap<LessonLevelCreateDTO, LessonLevel>();
		}
	}
}
