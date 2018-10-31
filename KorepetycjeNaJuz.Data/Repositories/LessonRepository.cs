﻿using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Infrastructure.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {
        private readonly KorepetycjeContext _context;

        public LessonRepository(KorepetycjeContext context) : base(context)
        {
            _context = context;
        }

        public override void Update(Lesson entity)
        {
            var former = _context.Lessons.Find(entity.Id);

            former.LessonStatusId = entity.LessonStatusId == 0 ? former.LessonStatusId : entity.LessonStatusId;
            former.LessonStatus = entity.LessonStatus ?? former.LessonStatus;
            former.NumberOfHours = entity.NumberOfHours;
            former.OpinionOfCoach = entity.OpinionOfCoach ?? former.OpinionOfCoach;
            former.OpinionOfStudent = entity.OpinionOfStudent ?? former.OpinionOfStudent;
            former.RatingOfCoach = entity.RatingOfCoach ?? former.RatingOfCoach;
            former.RatingOfStudent = entity.RatingOfStudent ?? former.RatingOfStudent;
            former.StudentId = entity.StudentId == 0 ? former.StudentId : entity.StudentId;
            former.Student = entity.Student ?? former.Student;
            former.CoachAddress = entity.CoachAddress ?? former.CoachAddress;
            former.CoachAddressId = entity.CoachAddressId == 0 ? former.CoachAddressId : entity.CoachAddressId;
            former.CoachLesson = entity.CoachLesson ?? former.CoachLesson;
            former.Date = entity.Date;

            _context.SaveChanges();
        }

        public override async Task UpdateAsync(Lesson entity)
        {
            var former = _context.Lessons.Find(entity.Id);

            former.LessonStatusId = entity.LessonStatusId == 0 ? former.LessonStatusId : entity.LessonStatusId;
            former.LessonStatus = entity.LessonStatus ?? former.LessonStatus;
            former.NumberOfHours = entity.NumberOfHours;
            former.OpinionOfCoach = entity.OpinionOfCoach ?? former.OpinionOfCoach;
            former.OpinionOfStudent = entity.OpinionOfStudent ?? former.OpinionOfStudent;
            former.RatingOfCoach = entity.RatingOfCoach ?? former.RatingOfCoach;
            former.RatingOfStudent = entity.RatingOfStudent ?? former.RatingOfStudent;
            former.StudentId = entity.StudentId == 0 ? former.StudentId : entity.StudentId;
            former.Student = entity.Student ?? former.Student;
            former.CoachAddress = entity.CoachAddress ?? former.CoachAddress;
            former.CoachAddressId = entity.CoachAddressId == 0 ? former.CoachAddressId : entity.CoachAddressId;
            former.CoachLesson = entity.CoachLesson ?? former.CoachLesson;
            former.Date = entity.Date;

            await _context.SaveChangesAsync();
        }

        //public Lesson Add(Lesson entity)
        //{
        //    _context.Lessons.Add(entity);
        //    _context.SaveChanges();

        //    return entity;
        //}

        //public async Task<Lesson> AddAsync(Lesson entity)
        //{
        //    _context.Lessons.Add(entity);
        //    await _context.SaveChangesAsync();

        //    return entity;
        //}

        //public void Delete(Lesson entity)
        //{
        //    _context.Lessons.Remove(entity);
        //    _context.SaveChanges();
        //}

        //public void Delete(int id)
        //{
        //    var lesson = _context.Lessons.FirstOrDefault(x => x.Id == id);
        //    _context.Lessons.Remove(lesson);
        //    _context.SaveChanges();
        //}

        //public async Task DeleteAsync(Lesson entity)
        //{
        //    _context.Lessons.Remove(entity);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    var lesson = _context.Lessons.FirstOrDefault(x => x.Id == id);
        //    _context.Lessons.Remove(lesson);
        //    await _context.SaveChangesAsync();
        //}

        //public void DeleteRange(IEnumerable<Lesson> objects)
        //{
        //    _context.Lessons.RemoveRange(objects);
        //    _context.SaveChanges();
        //}

        //public async Task DeleteRangeAsync(IEnumerable<Lesson> objects)
        //{
        //    _context.Lessons.RemoveRange(objects);
        //    await _context.SaveChangesAsync();
        //}

        //public Lesson GetById(int id)
        //{
        //    return _context.Lessons.Find(id);
        //}

        //public async Task<Lesson> GetByIdAsync(int id)
        //{
        //    return await _context.Lessons.FindAsync(id);
        //}

        //public IEnumerable<Lesson> ListAll()
        //{
        //    return _context.Lessons.ToList();
        //}

        //public async Task<List<Lesson>> ListAllAsync()
        //{
        //    return await Task.Run(() => _context.Lessons.ToList());
        //}

        //public IQueryable<Lesson> Query()
        //{
        //    return _context.Lessons.AsQueryable();
        //}


    }
}
