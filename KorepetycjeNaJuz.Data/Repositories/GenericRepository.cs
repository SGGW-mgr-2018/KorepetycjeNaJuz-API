using KorepetycjeNaJuz.Core.Interfaces;

namespace KorepetycjeNaJuz.Infrastructure.Repositories
{
    public class GenericRepository<T> : RepositoryWithTypedId<T, int> where T : class, IEntityWithTypedId<int>
    {
        public GenericRepository( KorepetycjeContext appDbContext ) : base( appDbContext ) { }
    }
}
