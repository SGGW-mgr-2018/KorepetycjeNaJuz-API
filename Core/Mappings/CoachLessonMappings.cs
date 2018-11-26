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
                .ForMember(x => x.LessonSubject, opts => opts.MapFrom(i => i.Subject.Name));

            CreateMap<CoachLessonLevel, CoachLessonLevelDTO>()
                .ForMember(x => x.Id, opts => opts.MapFrom(i => i.LessonLevel.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(i => i.LessonLevel.LevelName));

            CreateMap<CoachLessonLevelDTO, CoachLessonLevel>().ReverseMap();

            CreateMap<CoachLessonDTO, CoachLesson>()
                .ForMember(x => x.RatePerHour, opts => opts.MapFrom(i => i.RatePerHour))
                .ForMember(x => x.CoachId, opts => opts.MapFrom(i => i.CoachId))
                .ForMember(x => x.LessonSubjectId, opts => opts.MapFrom(i => i.LessonSubjectId))
                .ForMember(x => x.LessonLevels, opts => opts.Ignore());
        }
    }
}
