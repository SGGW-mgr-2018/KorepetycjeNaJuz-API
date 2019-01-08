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

            CreateMap<Lesson, LessonDTO>().ReverseMap();

            CreateMap<Lesson, LessonStudentDTO>()
                .ForMember(x => x.LessonStatusName, opts => opts.MapFrom(i => i.LessonStatus.Name));
        }
    }
}
