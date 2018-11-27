﻿using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Enums;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Infrastructure.Services
{
    public class CoachLessonService : ICoachLessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICoachLessonRepository _coachLessonRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ILessonLevelRepository _lessonLevelRepository;
        private readonly IMapper _mapper;

        public CoachLessonService(
            ICoachLessonRepository coachLessonRepository,
            ILessonRepository lessonRepository,
            IAddressRepository addressRepository,
            ILessonLevelRepository lessonLevelRepository,
            IMapper mapper)
        {
            _coachLessonRepository = coachLessonRepository;
            _lessonRepository = lessonRepository;
            _addressRepository = addressRepository;
            _lessonLevelRepository = lessonLevelRepository;
            _mapper = mapper;
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
        }

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

        public bool IsTimeAvailable(int coachID, DateTime startDate, DateTime endDate)
        {
            return !_coachLessonRepository.Query()
                .Where(c => c.CoachId == coachID)
                .Any(x => ((x.DateStart >= startDate && x.DateStart <= endDate) || (x.DateEnd >= startDate && x.DateEnd <= endDate)));
        }

        public async Task AddCoachLesson(AddCoachLessonDTO coachLessonDTO)
        {
            Address address = _mapper.Map<Address>(coachLessonDTO.Address);
            address.CoachId = coachLessonDTO.CoachId;
            Address dbAddress = _addressRepository.Query()
                .Where(add => add.CoachId == coachLessonDTO.CoachId)
                .Where(add => add.Latitude == address.Latitude && add.Longitude == address.Longitude && add.City == address.City && add.Street == address.Street).FirstOrDefault();

            address = dbAddress == null 
                ? await _addressRepository.AddAsync(address) 
                : address = dbAddress;

            List<CoachLesson> coachLessonList = new List<CoachLesson>();

            DateTime currentDate = coachLessonDTO.DateStart;
            ICollection<CoachLessonLevelDTO> levels = coachLessonDTO.LessonLevels;

            while (currentDate.AddMinutes(coachLessonDTO.Time).Subtract(coachLessonDTO.DateEnd).TotalMinutes <= 0)
            {
                CoachLesson coachLesson = _mapper.Map<CoachLesson>(coachLessonDTO);
                coachLesson.DateStart = currentDate;
                currentDate = currentDate.AddMinutes(coachLessonDTO.Time);
                coachLesson.DateEnd = currentDate;
                coachLesson.AddressId = address.Id;
                coachLesson.LessonStatusId = (int)LessonStatuses.WaitingForStudents;

                coachLessonList.Add(coachLesson);
            }

            foreach (var item in coachLessonList)
            {
                ICollection<CoachLessonLevel> mappedLevels = new List<CoachLessonLevel>();
                foreach (var level in levels)
                {
                    CoachLessonLevel a = new CoachLessonLevel { CoachLessonId = item.Id, LessonLevelId = level.Id };
                    mappedLevels.Add(a);
                }
                item.LessonLevels = mappedLevels;
                await _coachLessonRepository.AddAsync(item);
            }
        }
    }
}
