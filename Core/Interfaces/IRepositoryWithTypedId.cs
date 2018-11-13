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
        Task<T> AddAsync( T entity );
        T Update( T entity );
        Task<T> UpdateAsync( T entity );
        int Delete( T entity );
        Task<int> DeleteAsync( T entity );
        int Delete( Tid id );
        Task<int> DeleteAsync( Tid id );
        int DeleteRange( IEnumerable<T> objects );
        Task<int> DeleteRangeAsync( IEnumerable<T> objects );
        IQueryable<T> Query();
    }
}
