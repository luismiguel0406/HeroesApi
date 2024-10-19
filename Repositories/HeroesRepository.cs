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

        public void Add(Heroes hero)
        {
              _context.Heroes.Add(hero);
        }

        public void Delete(string id)
        {
           var hero = _context.Heroes.Find(id);
            if (hero != null) { 
            _context.Heroes.Update(hero);          
            }
            return;
        }

        public void Update(Heroes hero) { 
            _context.Heroes.Update(hero);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
