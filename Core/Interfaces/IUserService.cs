using KorepetycjeNaJuz.Core.Models;
using System.Collections.Generic;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface IUserService
    {
        User Authenticate( string username, string password );
        IEnumerable<User> GetAll();
    }
}
