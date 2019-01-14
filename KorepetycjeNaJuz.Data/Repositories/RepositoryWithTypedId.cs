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
            this._dbContext = dbContext;
            this._dbSet = this._dbContext.Set<T>();
        }

        public virtual T Add( T entity )
        {
            T result = this._dbContext.Add( entity ).Entity;
            this._dbContext.SaveChanges();
            return result;
        }

        public virtual async Task<T> AddAsync( T entity )
        {
            T result = this._dbContext.Add( entity ).Entity;
            await this._dbContext.SaveChangesAsync();
            return result;
        }

        public virtual int Delete( T entity )
        {
            this._dbContext.Remove( entity );
            return this._dbContext.SaveChanges();
        }

        public virtual int Delete( Tid id )
        {
            this._dbContext.Remove( this._dbSet.Find( id ) );
            return this._dbContext.SaveChanges();
        }

        public virtual async Task<int> DeleteAsync( T entity )
        {
            this._dbContext.Remove( entity );
            return await this._dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> DeleteAsync( Tid id )
        {
            this._dbContext.Remove( this._dbContext.Set<T>().Find( id ) );
            return await this._dbContext.SaveChangesAsync();
        }

        public virtual T GetById( Tid id )
        {
            return this._dbSet.Find( id );
        }

        public virtual async Task<T> GetByIdAsync( Tid id )
        {
            return await this._dbSet.FindAsync( id );
        }

        public virtual IEnumerable<T> ListAll()
        {
            return this._dbSet.AsEnumerable();
        }

        public virtual async Task<List<T>> ListAllAsync()
        {
            return await this._dbSet.ToListAsync();
        }

        public virtual T Update( T entity )
        {
            this._dbContext.Entry( entity ).State = EntityState.Modified;
            T result = this._dbSet.Update( entity ).Entity;
            this._dbContext.SaveChanges();
            return result;
        }

        public virtual async Task<T> UpdateAsync( T entity )
        {
            T result = this._dbSet.Update( entity ).Entity;
            await this._dbContext.SaveChangesAsync();
            return result;
        }

        public virtual IQueryable<T> Query()
        {
            return this._dbSet;
        }

        public virtual int DeleteRange( IEnumerable<T> objects )
        {
            this._dbContext.RemoveRange( objects );
            return this._dbContext.SaveChanges();
        }

        public virtual async Task<int> DeleteRangeAsync( IEnumerable<T> objects )
        {
            this._dbContext.RemoveRange( objects );
            return await this._dbContext.SaveChangesAsync();
        }

        public void EnableLazyLoading()
        {
            this._dbContext.ChangeTracker.LazyLoadingEnabled = true;
        }

        public void DisableLazyLoading()
        {
            this._dbContext.ChangeTracker.LazyLoadingEnabled = false;
        }
    }
}