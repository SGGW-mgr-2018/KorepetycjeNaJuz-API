using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System;
using System.Collections.Generic;

namespace KorepetycjeNaJuz.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWithTypedId<Users, int> _usersRepository;
        public Users Authenticate( string username, string password )
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Users> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
