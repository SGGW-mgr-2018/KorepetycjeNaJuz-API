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
            CreateMap<CoachLesson, CoachLessonDTO>()
                .ForMember(x => x.LessonSubject, opts => opts.MapFrom(i => i.Subject.Name))
                .ForMember(x => x.Latitude, opts => opts.MapFrom(i => i.Address.Latitude))
                .ForMember(x => x.Longitude, opts => opts.MapFrom(i => i.Address.Longitude))
                .ForMember(x => x.City, opts => opts.MapFrom(i => i.Address.City))
                .ForMember(x => x.Street, opts => opts.MapFrom(i => i.Address.Street));

            CreateMap<CoachLessonLevel, CoachLessonLevelDTO>()
                .ForMember(x => x.Id, opts => opts.MapFrom(i => i.LessonLevel.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(i => i.LessonLevel.LevelName));
        }
    }
}
