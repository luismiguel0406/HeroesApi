using HeroesApi.Interfaces;
using HeroesApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IEnumerable<Heroes>> GetHeroes() {
            return  await _context.Heroes.ToListAsync();
        }

        public async Task<Heroes> GetHeroById( string id)
        {
            var hero = await _context.Heroes.FindAsync(id);        
            return hero;
        }

        public async Task<IEnumerable<Heroes>> GetSuggestion(string suggestion)
        {
            var query = await _context.Heroes.Where(heroes => heroes.Superhero.Contains(suggestion)).ToListAsync();
            return query;
        }

    }
}
