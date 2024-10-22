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
            var heroes = await _context.Heroes.AsNoTracking().ToListAsync();
            var query = from resultHeroes in heroes
                        where resultHeroes.IsActive
                        select resultHeroes;
            return query;
        }

        public async Task<Heroes> GetById( string id)
        {
            return  await _context.Heroes.SingleAsync(hero=>hero.Id == id && hero.IsActive);  
        }

        public async Task<IEnumerable<Heroes>> GetSuggestion(string suggestion)
        {
            return await _context.Heroes.Where(heroes => heroes.Superhero.Contains(suggestion)).ToListAsync();
           
        }

        public void Add(Heroes hero)
        {
              _context.Heroes.Add(hero);
        }

        public async Task Delete(string id)
        {
           var hero = await _context.Heroes.FirstOrDefaultAsync(hero=> hero.Id == id);
            if (hero == null) return;

            hero.IsActive = false;
           
        }

        public void Update(string id, Heroes hero) { 

            _context.Heroes.Update(hero);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
