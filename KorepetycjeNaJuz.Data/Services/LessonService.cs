using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Enums;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Infrastructure.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICoachLessonRepository _coachLessonRepository;
        private readonly IMessageService _messageService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LessonService(
            ILessonRepository lessonRepository,
            ICoachLessonRepository coachLessonRepository,
            IMessageService messageService,
            IUserRepository userRepository,
            IMapper mapper)
        {
            this._lessonRepository = lessonRepository;
            this._coachLessonRepository = coachLessonRepository;
            this._messageService = messageService;
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task CreateLessonAsync(LessonCreateDTO lessonCreateDTO)
        {
            var lesson = _mapper.Map<Lesson>(lessonCreateDTO);
            var coachLesson = await _coachLessonRepository.GetByIdAsync(lessonCreateDTO.CoachLessonId);

            lesson.Date = coachLesson.DateStart;
            lesson.NumberOfHours = (float)(coachLesson.DateEnd - coachLesson.DateStart).TotalHours;
            lesson.LessonStatusId = (int)LessonStatuses.Reserved;
            await _lessonRepository.AddAsync(lesson);

            coachLesson.LessonStatusId = (int)LessonStatuses.Reserved;
            await _coachLessonRepository.UpdateAsync(coachLesson);

            // Symulacja powiadomienia 'Wiadomosc od uzytkownika system'
            var student = await _userRepository.GetByIdAsync(lesson.StudentId);
            var studentFirstName = student.FirstName;
            var studentLastNamePrefix = student.LastName.Trim();
            studentLastNamePrefix = studentLastNamePrefix.Length > 1 ? studentLastNamePrefix.First().ToString().ToUpper() + "." : "";
            var content = $"Użytkownik {studentFirstName} {studentLastNamePrefix} zapisał/a się na Twoją lekcję ({coachLesson.Subject.Name}) o {coachLesson.DateStart.ToString("yyyy-MM-dd HH:mm")}";
            var message = new Message
            {
                Content = content,
                OwnerId = 0,
                RecipientId = coachLesson.CoachId
            };
            await _messageService.AddMessageAsync(message);
        }

        public bool IsLessonExists(int lessonId)
        {
            return _lessonRepository.GetById(lessonId) != null;
        }

        public void RejectLesson(int id)
        {
            var lesson = _lessonRepository.GetById(id);

            lesson.LessonStatusId = (int)LessonStatuses.Rejected;
            _lessonRepository.UpdateAsync(lesson);
        }

        public Lesson GetById(int id)
        {
            return _lessonRepository.GetById(id);
        }

        public void ApproveLesson(int id)
        {
            var lesson = _lessonRepository.GetById(id);
            lesson.LessonStatusId = (int)LessonStatuses.Approved;
            lesson.CoachLesson.LessonStatusId = (int)LessonStatuses.Approved;

            _lessonRepository.Update(lesson);
        }

        public IEnumerable<LessonStudentDTO> GetLessonsForCoachLesson(int coachLessonId)
        {
            var coachLesson = _coachLessonRepository.GetById(coachLessonId);

            return _mapper.Map<IEnumerable<LessonStudentDTO>>(coachLesson.Lessons);
        }

        public void RateLessonStudent(LessonRatingDTO lessonRatingDTO)
        {
            var lesson = _lessonRepository.GetById(lessonRatingDTO.LessonId);
            lesson.RatingOfStudent = (byte) lessonRatingDTO.Rating;
            lesson.OpinionOfStudent = lessonRatingDTO.Opinion;

            _lessonRepository.Update(lesson);
        }

        public void RateLessonCoach(LessonRatingDTO lessonRatingDTO)
        {
            var lesson = _lessonRepository.GetById(lessonRatingDTO.LessonId);
            lesson.RatingOfCoach = (byte)lessonRatingDTO.Rating;
            lesson.OpinionOfCoach = lessonRatingDTO.Opinion;

            _lessonRepository.Update(lesson);
        }
    }
}
