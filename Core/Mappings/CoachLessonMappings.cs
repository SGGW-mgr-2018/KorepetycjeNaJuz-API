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
                .ForMember(x => x.Time, opts => opts.MapFrom(i => i.DateEnd.Subtract(i.DateStart).TotalMinutes))
                .ForMember(x => x.Description, opts => opts.NullSubstitute(string.Empty));

            CreateMap<CoachLessonLevel, CoachLessonLevelDTO>()
                .ForMember(x => x.Id, opts => opts.MapFrom(i => i.LessonLevel.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(i => i.LessonLevel.LevelName));

            CreateMap<CoachLessonCreateDTO, CoachLesson>()
                .ForMember(x => x.LessonLevels, opts => opts.Ignore());

            CreateMap<CoachLesson, CoachLessonCalendarDTO>()
                .ForMember(x => x.MyLesson, opts => opts.Ignore())
                .ForMember(x => x.Lessons, opts => opts.MapFrom(i => i.Lessons))
                .ForMember(x => x.Time, opts => opts.MapFrom(i => i.DateEnd.Subtract(i.DateStart).TotalMinutes))
                .ForMember(x => x.UserRole, opts => opts.Ignore());
           
        }
    }
}
