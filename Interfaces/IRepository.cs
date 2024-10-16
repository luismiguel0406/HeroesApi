namespace HeroesApi.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity?> GetById(string id);

        IQueryable<TEntity> GetQuereyable();

        
    }
}
