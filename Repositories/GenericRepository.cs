using HeroesApi.Interfaces;
using HeroesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroesApi.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DcComicsDb _context;
        public GenericRepository( DcComicsDb context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetById(string id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> GetQuereyable()
        {
            return _context.Set<TEntity>();
        }
    }
}
