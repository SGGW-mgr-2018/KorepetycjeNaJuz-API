using KorepetycjeNaJuz.Core.Interfaces;
using KorepetycjeNaJuz.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(KorepetycjeContext context)
            : base(context)
        {

        }

        public async Task AcceptCookies(int userId)
        {
            var user = await _dbSet.FirstAsync(u => u.Id == userId);
            user.CookiesConfirmed = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task AcceptPrivacy(int userId)
        {
            var user = await _dbSet.FirstAsync(u => u.Id == userId);
            user.PrivacyPolicesConfirmed = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task AcceptRODO(int userId)
        {
            var user = await _dbSet.FirstAsync(u => u.Id == userId);
            user.RodoConfirmed = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task ClearPolicyAcceptanceAsync()
        {
            var users = await _dbSet.ToListAsync();
            users.ForEach(u =>
            {
                u.CookiesConfirmed = false;
                u.PrivacyPolicesConfirmed = false;
                u.RodoConfirmed = false;
            });
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<User>> FindByAsync(Expression<Func<User, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
