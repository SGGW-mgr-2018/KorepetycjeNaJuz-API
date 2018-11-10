using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorepetycjeNaJuz.Core.Interfaces
{
    public interface IRepositoryWithTypedId<T, Tid> where T : IEntityWithTypedId<Tid>
    {
        T GetById( Tid id );
        Task<T> GetByIdAsync( Tid id );
        Task<List<T>> ListAllAsync();
        IEnumerable<T> ListAll();
        T Add( T entity );
        Task AddAsync( T entity );
        void Update( T entity );
        Task UpdateAsync( T entity );
        void Delete( T entity );
        Task DeleteAsync( T entity );
        void Delete( Tid id );
        Task DeleteAsync( Tid id );
        void DeleteRange( IEnumerable<T> objects );
        Task DeleteRangeAsync( IEnumerable<T> objects );
        IQueryable<T> Query();
    }
}
