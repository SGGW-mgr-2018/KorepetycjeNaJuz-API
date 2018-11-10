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

        public virtual Task AddAsync( T entity )
        {
            this._dbContext.Add( entity );
            return this._dbContext.SaveChangesAsync();
        }

        public virtual void Delete( T entity )
        {
            this._dbContext.Remove( entity );
            this._dbContext.SaveChanges();
        }

        public virtual void Delete( Tid id )
        {
            this._dbContext.Remove( this._dbSet.Find( id ) );
            this._dbContext.SaveChanges();

        }

        public virtual Task DeleteAsync( T entity )
        {
            this._dbContext.Remove( entity );
            return this._dbContext.SaveChangesAsync();
        }

        public virtual Task DeleteAsync( Tid id )
        {
            this._dbContext.Remove( this._dbContext.Set<T>().Find( id ) );
            return this._dbContext.SaveChangesAsync();
        }

        public virtual T GetById( Tid id )
        {
            return this._dbSet.Find( id );
        }

        public virtual Task<T> GetByIdAsync( Tid id )
        {
            return this._dbSet.FindAsync( id );
        }

        public virtual IEnumerable<T> ListAll()
        {
            return this._dbSet.AsEnumerable();
        }

        public virtual Task<List<T>> ListAllAsync()
        {
            return this._dbSet.ToListAsync();
        }

        public virtual void Update( T entity )
        {
            this._dbContext.Entry( entity ).State = EntityState.Modified;
            this._dbSet.Update( entity );
            this._dbContext.SaveChanges();
        }

        public virtual Task UpdateAsync( T entity )
        {
            this._dbSet.Update( entity );
            return this._dbContext.SaveChangesAsync();

        }

        public virtual IQueryable<T> Query()
        {
            return this._dbSet;
        }

        public virtual void DeleteRange( IEnumerable<T> objects )
        {
            this._dbContext.RemoveRange( objects );
            this._dbContext.SaveChanges();
        }

        public virtual Task DeleteRangeAsync( IEnumerable<T> objects )
        {
            this._dbContext.RemoveRange( objects );
            return this._dbContext.SaveChangesAsync();
        }
    }
}