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
                .ForMember(x => x.Time, opts => opts.MapFrom(i => i.DateEnd.Subtract(i.DateStart).TotalMinutes));

            CreateMap<CoachLessonLevel, CoachLessonLevelDTO>()
                .ForMember(x => x.Id, opts => opts.MapFrom(i => i.LessonLevel.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(i => i.LessonLevel.LevelName));

            CreateMap<CoachLessonCreateDTO, CoachLesson>()
                .ForMember(x => x.LessonLevels, opts => opts.Ignore());
        }
    }
}
