using HeroesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeroesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly DcComicsDb _context;

        public HeroesController(DcComicsDb context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Heroes>>> GetHeroes()
        {

            var heroes = await _context.Heroes.ToListAsync();
            return Ok(heroes);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Heroes>> GetHeroById(string id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }
            return Ok(hero);
        }
        [HttpGet("suggest")]
        public async Task<ActionResult<IEnumerable<Heroes>>> GetSuggestion( string suggestion)
        {
            var query = await _context.Heroes.Where(heroes => heroes.Superhero.Contains(suggestion)).ToListAsync();
            if(query.Count == 0) return NotFound();
            return Ok(query);
           
        }

    }
}
