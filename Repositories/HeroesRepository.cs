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
                 return await _context.Heroes
                .Where(heroes => !string.IsNullOrEmpty(heroes.Superhero) && heroes.Superhero.Contains(suggestion) )
                .ToListAsync();
    
        }

        public void Add(Heroes hero)
        {
              _context.Heroes.Add(hero);
        }

        public async Task  Delete(string id)
        {
            await _context.Heroes.Where(heroes=>heroes.Id == id)
                .ExecuteUpdateAsync(existingObject => existingObject
                                   .SetProperty(property => property.IsActive , false));        
        }
        public async Task Update(string id, Heroes hero) {

            await _context.Heroes.Where(heroes => heroes.Id == id)
                         .ExecuteUpdateAsync(heroFound => heroFound
                         .SetProperty(p => p.Superhero, hero.Superhero)
                         .SetProperty(p => p.AlterEgo, hero.AlterEgo)
                         .SetProperty(p => p.Publisher, hero.Publisher)
                         .SetProperty(p => p.FirstAppearance, hero.FirstAppearance)
                         .SetProperty(p => p.ImageUrl, hero.ImageUrl)
                         .SetProperty(p => p.Characters, hero.Characters));               
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
