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
        Task ClearPolicyAcceptanceAsync();
        Task AcceptCookies(int userId);
        Task AcceptRODO(int userId);
        Task AcceptPrivacy(int userId);
    }
}
