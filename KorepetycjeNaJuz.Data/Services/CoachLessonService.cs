using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Enums;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KorepetycjeNaJuz.Infrastructure.Services
{
    public class CoachLessonService : ICoachLessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICoachLessonRepository _coachLessonRepository;
        private readonly IMapper _mapper;

        public CoachLessonService(
            ICoachLessonRepository coachLessonRepository,
            ILessonRepository lessonRepository,
            IMapper mapper)
        {
            this._coachLessonRepository = coachLessonRepository;
            this._lessonRepository = lessonRepository;
            this._mapper = mapper;
        }

        public bool IsUserAlreadySignedUpForCoachLesson(int coachLessonId, int userId)
        {
            var lesson = _lessonRepository.Query()
                                          .Where(x =>
                                            x.CoachLessonId == coachLessonId && 
                                            x.StudentId == userId)
                                          .FirstOrDefault();
            return lesson != null;
        }

        public bool IsCoachLessonAvailable(int coachLessonId)
        {
            var coachLesson = _coachLessonRepository.GetById(coachLessonId);
            var coachLessonStatus = (LessonStatuses)coachLesson.LessonStatusId;

            return coachLessonStatus == LessonStatuses.WaitingForStudents ||
                   coachLessonStatus == LessonStatuses.Reserved;
        }

        public bool IsCoachLessonExists(int coachLessonId)
        {
            return _coachLessonRepository.GetById(coachLessonId) != null;
        }

        public bool IsUserOwnerOfCoachLesson(int coachLessonId, int userId)
        {
            var coachLesson = _coachLessonRepository.GetById(coachLessonId);
            return coachLesson.CoachId == userId;
;       }

        public IEnumerable<CoachLessonDTO> GetCoachLessonsByFilters(CoachLessonsByFiltersDTO filters)
        {
            List<CoachLessonDTO> output;

            if (filters.DateFrom == null)
                filters.DateFrom = DateTime.Now;

            if (filters.DateTo == null)
                filters.DateTo = filters.DateFrom.Value.AddDays(1);

            IQueryable<CoachLesson> query = _coachLessonRepository.Query().Where(x =>
                (x.LessonStatus.Id == (int)LessonStatuses.WaitingForStudents ||
                 x.LessonStatus.Id == (int)LessonStatuses.Reserved) &&
                x.DateStart >= filters.DateFrom &&
                x.DateEnd <= filters.DateTo);

            // po tytule
            if (filters.SubjectId != null)
                query = query.Where(x => x.Subject.Id == filters.SubjectId.Value);

            // po poziomie
            if (filters.LevelId != null)
                query = query.Where(x => x.LessonLevels.Any(i => i.LessonLevel.Id == filters.LevelId.Value));

            // po coachID
            if (filters.CoachId != null)
                query = query.Where(x => x.CoachId == filters.CoachId.Value);

            if (filters.Latitiude == null || filters.Longitiude == null || filters.Radius == null)
            {
                // dodajemy wszystkie
                output = _mapper.Map<List<CoachLessonDTO>>(query.ToList());
            }
            else
            {
                output = new List<CoachLessonDTO>();
                foreach (CoachLesson coachLesson in query)
                {
                    // po odległości
                    var distance = Distance(coachLesson.Address.Latitude, coachLesson.Address.Longitude, filters.Latitiude.Value, filters.Longitiude.Value);
                    if (distance <= filters.Radius)
                    {
                        var coachLessonDTO = _mapper.Map<CoachLessonDTO>(coachLesson);
                        output.Add(coachLessonDTO);
                    }
                }
            }

            return output;
        }

        private double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(Deg2rad(lat1)) * Math.Sin(Deg2rad(lat2)) + Math.Cos(Deg2rad(lat1)) * Math.Cos(Deg2rad(lat2)) * Math.Cos(Deg2rad(theta));
            dist = Math.Acos(dist);
            dist = Rad2deg(dist);
            dist = dist * 60 * 1.1515;
            dist = dist * 1.609344; // to kilomiters
            return (dist);
        }

        private double Deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double Rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
