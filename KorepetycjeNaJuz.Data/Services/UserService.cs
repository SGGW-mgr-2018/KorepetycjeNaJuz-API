﻿using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Linq;
using NLog;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using KorepetycjeNaJuz.Core.Enums;

namespace KorepetycjeNaJuz.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ILogger _logger;

        public UserService(
            IUserRepository userRepository, 
            IMapper mapper, 
            UserManager<User> userManager)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._userManager = userManager;
            this._logger = LogManager.GetLogger("apiLogger");
        }

        public void CalculateCoachRating(int userId)
        {
            _userRepository.DisableLazyLoading();

            var user = _userRepository
                .Query()
                .Include(x => x.CoachLessonsAsTeacher)
                    .ThenInclude((CoachLesson x) => x.Lessons)
                .Where(x => x.Id == userId)
                .FirstOrDefault();

            var endedCoachLessons = user.CoachLessonsAsTeacher.Where(x => x.LessonStatusId == (int)LessonStatuses.Approved);
            float rating = 0;
            if (endedCoachLessons.Count() > 0)
            {
                int numberOfLessonWithRating = 0;
                foreach (var coachLesson in endedCoachLessons)
                {
                    var lesson = coachLesson.Lessons.Where(x => x.LessonStatusId == (int)LessonStatuses.Approved && x.RatingOfCoach != null).FirstOrDefault();
                    if (lesson != null)
                    {
                        numberOfLessonWithRating++;
                        rating += (float)lesson.RatingOfCoach;
                    }
                }
                if (numberOfLessonWithRating > 0)
                {
                    rating = rating / numberOfLessonWithRating;
                }

                user.CoachRating = rating;
            }
            _userRepository.SaveChanges();
            _userRepository.EnableLazyLoading();
        }

        public async Task<bool> CreateUserAsync(UserCreateDTO userCreateDTO)
        {
            var user = _mapper.Map<User>(userCreateDTO);
            var result = await _userManager.CreateAsync(user, userCreateDTO.Password);
           
            // Log Errors
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(x => x.Description));
                _logger.Error("Errors during UserService.CreateUser(): " + errors);
            }

            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id) > 0 ? true : false;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.ListAllAsync();
            users = users.Where(x => x.Id != 0).ToList(); // Pominięcie użytkownika SYSTEM
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            var user = await _userRepository.FindByAsync(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));

            return user.Any();
        }

        public async Task<bool> IsUserExistsAsync(int id)
        {
            return await GetUserAsync(id) != null;
        }

        public async Task<UserDTO> UpdateUserAsync(UserEditDTO userEditDTO)
        {
            var user = await _userRepository.GetByIdAsync(userEditDTO.Id);
            user = _mapper.Map(userEditDTO, user);

            if (userEditDTO.Password != null)
            {
                var newPassword = _userManager.PasswordHasher.HashPassword(user, userEditDTO.Password);
                user.PasswordHash = newPassword;
            }

            await _userRepository.UpdateAsync(user);

            return _mapper.Map<UserDTO>(user);
        }
    }
}
