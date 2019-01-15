using KorepetycjeNaJuz.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserAsync(int id);
        Task<bool> CreateUserAsync(UserCreateDTO userCreateDTO);
        Task<bool> IsEmailExistsAsync(string email);
        Task<bool> IsUserExistsAsync(int id);
        Task<UserDTO> UpdateUserAsync(UserEditDTO userEditDTO);
        Task<bool> DeleteUserAsync(int id);
        void CalculateCoachRating(int userId);
    }
}
