using AutoMapper;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Core.DTO;
using System;

namespace KorepetycjeNaJuz.Core.Mappings
{
    public class CoachLessonMappings : Profile
    {
        public CoachLessonMappings()
        {
            CreateMap<CoachLessonDTO, CoachLesson>()
                .ForPath(x => x.Subject.Name, opts => opts.MapFrom(i => i.LessonSubject));
            CreateMap<CoachLessonDTO, CoachLesson>()
                .ForPath(x => x.Address.Latitude, opts => opts.MapFrom(i => i.Latitude));
            CreateMap<CoachLessonDTO, CoachLesson>()
                .ForPath(x => x.Address.Longitude, opts => opts.MapFrom(i => i.Longitude));
            CreateMap<CoachLessonDTO, CoachLesson>()
                .ForPath(x => x.Address.City, opts => opts.MapFrom(i => i.City));
            CreateMap<CoachLessonDTO, CoachLesson>()
                .ForPath(x => x.Address.Street, opts => opts.MapFrom(i => i.Street));

            CreateMap<CoachLesson, CoachLessonDTO>().ReverseMap(); // Two-way map
        }
    }
}
