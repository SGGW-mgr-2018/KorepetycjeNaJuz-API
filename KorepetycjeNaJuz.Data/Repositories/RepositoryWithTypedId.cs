using KorepetycjeNaJuz.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Infrastructure.Repositories
{
    public class RepositoryWithTypedId<T, Tid> : IRepositoryWithTypedId<T, Tid> where T : class, IEntityWithTypedId<Tid>
    {
        protected readonly KorepetycjeContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        public RepositoryWithTypedId( KorepetycjeContext dbContext )
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual T Add( T entity )
        {
            T result = _dbContext.Add( entity ).Entity;
            _dbContext.SaveChanges();
            return result;
        }

        public virtual async Task<T> AddAsync( T entity )
        {
            T result = _dbContext.Add( entity ).Entity;
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(result);
        }

        public virtual void Delete( T entity )
        {
            _dbContext.Remove( entity );
            _dbContext.SaveChanges();
        }

        public virtual void Delete( Tid id )
        {
            _dbContext.Remove( this._dbSet.Find( id ) );
            _dbContext.SaveChanges();

        }

        public virtual async Task DeleteAsync( T entity )
        {
            _dbContext.Remove( entity );
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync( Tid id )
        {
            _dbContext.Remove( this._dbContext.Set<T>().Find( id ) );
            await _dbContext.SaveChangesAsync();
        }

        public virtual T GetById( Tid id )
        {
            return _dbSet.Find( id );
        }

        public virtual async Task<T> GetByIdAsync( Tid id )
        {
            return await _dbSet.FindAsync( id );
        }

        public virtual IEnumerable<T> ListAll()
        {
            return _dbSet.AsEnumerable();
        }

        public virtual async Task<List<T>> ListAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual void Update( T entity )
        {
            _dbContext.Entry( entity ).State = EntityState.Modified;
            _dbSet.Update( entity );
            _dbContext.SaveChanges();
        }

        public virtual async Task UpdateAsync( T entity )
        {
            _dbSet.Update( entity );
            await _dbContext.SaveChangesAsync();

        }

        public virtual IQueryable<T> Query()
        {
            return _dbSet;
        }

        public virtual void DeleteRange( IEnumerable<T> objects )
        {
            _dbContext.RemoveRange( objects );
            _dbContext.SaveChanges();
        }

        public virtual async Task DeleteRangeAsync( IEnumerable<T> objects )
        {
            _dbContext.RemoveRange( objects );
            await _dbContext.SaveChangesAsync();
        }
    }
}