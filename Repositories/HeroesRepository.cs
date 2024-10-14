using HeroesApi.Interfaces;
using HeroesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroesApi.Repositories
{
    public class HeroesRepository :IHeroRepository
    {
        private  readonly DcComicsDb _context;
        public HeroesRepository(DcComicsDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Heroes>> GetAll() {
            return  await _context.Heroes.AsNoTracking().ToListAsync();
        }

        public async Task<Heroes> GetById( string id)
        {
            return  await _context.Heroes.SingleAsync(hero=>hero.Id == id);  
        }

        public async Task<IEnumerable<Heroes>> GetSuggestion(string suggestion)
        {
            return await _context.Heroes.Where(heroes => heroes.Superhero.Contains(suggestion)).ToListAsync();
           
        }

    }
}
