using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Enums;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Infrastructure.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICoachLessonRepository _coachLessonRepository;
        private readonly IMapper _mapper;

        public LessonRepository(KorepetycjeContext dbContext, 
            ILessonRepository lessonRepository,
            ICoachLessonRepository coachLessonRepository,
            IMapper mapper)
            : base(dbContext)
        {
            this._lessonRepository = lessonRepository;
            this._coachLessonRepository = coachLessonRepository;
            this._mapper = mapper;
        }

        public async Task CreateLessonAsync(LessonCreateDTO lessonCreateDTO)
        {
            var lesson = _mapper.Map<Lesson>(lessonCreateDTO);
            var coachLesson = await _coachLessonRepository.GetByIdAsync(lessonCreateDTO.CoachLessonId);

            lesson.Date = coachLesson.DateEnd;
            lesson.NumberOfHours = (coachLesson.DateEnd - coachLesson.DateStart).Hours;
            lesson.LessonStatusId = (int)LessonStatuses.Reserved;
            await _lessonRepository.AddAsync(lesson);

            coachLesson.LessonStatusId = (int)LessonStatuses.Reserved;
            await _coachLessonRepository.UpdateAsync(coachLesson);
        }

        public bool IsLessonExists(int id)
        {
            return _lessonRepository.GetById(id) != null;
        }
        public void RejectLesson(int id)
        {
            var lesson = _lessonRepository.GetById(id);

            lesson.LessonStatus.Id = (int)LessonStatuses.Rejected;
        }
    }
}
