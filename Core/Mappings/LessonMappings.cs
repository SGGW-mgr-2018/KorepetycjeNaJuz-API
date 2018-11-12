using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Models;

namespace KorepetycjeNaJuz.Core.Mappings
{
    public class LessonMappings : Profile
    {
        public LessonMappings()
        {
            CreateMap<LessonCreateDTO, Lesson>().ReverseMap();
        }
    }
}
