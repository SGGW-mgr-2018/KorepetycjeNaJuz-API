using KorepetycjeNaJuz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface IUserRepository : IRepositoryWithTypedId<User, int>
    {
        Task<IEnumerable<User>> FindByAsync(Expression<Func<User, bool>> predicate);
    }
}
