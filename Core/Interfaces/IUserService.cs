using KorepetycjeNaJuz.Core.Models;
using System.Collections.Generic;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface IUserService
    {
        Users Authenticate( string username, string password );
        IEnumerable<Users> GetAll();
    }
}
