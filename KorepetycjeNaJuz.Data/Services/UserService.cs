using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using System;
using System.Collections.Generic;

namespace KorepetycjeNaJuz.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWithTypedId<User, int> _usersRepository;
        public User Authenticate( string username, string password )
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
