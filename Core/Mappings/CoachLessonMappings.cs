using AutoMapper;
using KorepetycjeNaJuz.Core.Models;
using KorepetycjeNaJuz.Core.DTO;
using System;
using System.Linq;
using KorepetycjeNaJuz.Core.Enums;

namespace KorepetycjeNaJuz.Core.Mappings
{
    public class CoachLessonMappings : Profile
    {
        public CoachLessonMappings()
        {
            CreateMap<CoachLesson, CoachLessonDTO>()
                .ForMember(x => x.LessonSubject, opts => opts.MapFrom(i => i.Subject.Name))
                .ForMember(x => x.CoachRating, opts => opts.MapFrom(i => i.Coach.CoachRating))
                .ForMember(x => x.Time, opts => opts.MapFrom(i => i.DateEnd.Subtract(i.DateStart).TotalMinutes))
                .ForMember(x => x.Description, opts => opts.NullSubstitute(string.Empty));

            CreateMap<CoachLessonLevel, CoachLessonLevelDTO>()
                .ForMember(x => x.Id, opts => opts.MapFrom(i => i.LessonLevel.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(i => i.LessonLevel.LevelName));

            CreateMap<CoachLessonCreateDTO, CoachLesson>()
                .ForMember(x => x.LessonLevels, opts => opts.Ignore());

            CreateMap<CoachLesson, CoachLessonCalendarDTO>()
                .ForMember(x => x.Id, opts => opts.MapFrom(i => i.Id))
                .ForMember(x => x.UserRole, opts => opts.MapFrom(i => CoachLessonRole.Teacher))
                .ForMember(x => x.CoachId, opts => opts.MapFrom(i => i.CoachId))
                .ForMember(x => x.CoachFirstName, opts => opts.MapFrom(i => i.Coach.FirstName))
                .ForMember(x => x.CoachLastName, opts => opts.MapFrom(i => i.Coach.LastName))
                .ForMember(x => x.LessonStatusName, opts => opts.MapFrom(i => i.LessonStatus.Name))
                .ForMember(x => x.LessonLevels, opts => opts.MapFrom(i => i.LessonLevels))
                .ForMember(x => x.LessonSubjectId, opts => opts.MapFrom(i => i.LessonStatusId))
                .ForMember(x => x.LessonSubject, opts => opts.MapFrom(i => i.Subject.Name))
                .ForMember(x => x.RatePerHour, opts => opts.MapFrom(i => i.RatePerHour))
                .ForMember(x => x.Description, opts => opts.MapFrom(i => i.Description))
                .ForMember(x => x.DateStart, opts => opts.MapFrom(i => i.DateStart))
                .ForMember(x => x.DateEnd, opts => opts.MapFrom(i => i.DateEnd))
                .ForMember(x => x.Address, opts => opts.MapFrom(i => i.Address))
                .ForMember(x => x.Time, opts => opts.MapFrom(i => i.DateEnd.Subtract(i.DateStart).TotalMinutes))
                .ForMember(x => x.MyLesson, opts => opts.Ignore())
                .ForMember(x => x.Lessons, opts => opts.MapFrom(i => i.Lessons))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<CoachLesson, CoachLessonHistoryDTO>()
                .ForMember(x => x.SubjectName, opts => opts.MapFrom(i => i.Subject.Name))
                .ForMember(x => x.LessonLevelsName, opts => opts.MapFrom(i => string.Join(", ", i.LessonLevels.Select(x => x.LessonLevel.LevelName)).TrimEnd()))
                .ForMember(x => x.CoachFirstName, opts => opts.MapFrom(i => i.Coach.FirstName))
                .ForMember(x => x.CoachLastName, opts => opts.MapFrom(i => i.Coach.LastName))
                .ForMember(x => x.Time, opts => opts.MapFrom(i => i.DateEnd.Subtract(i.DateStart).TotalMinutes));
        }
    }
}
