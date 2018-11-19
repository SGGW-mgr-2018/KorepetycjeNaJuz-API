using AutoMapper;
using KorepetycjeNaJuz.Core.DTO;
using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using System.Linq;
using NLog;
using System.Collections.Generic;

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

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _userRepository.ListAllAsync();

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
    }
}
