using HeroesApi.Interfaces;
using HeroesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeroesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroRepository _heroRepository;

        public HeroesController(IHeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Heroes>>> GetHeroes()
        {
          var heroes =  await _heroRepository.GetHeroes();
            if (heroes == null) {
                NotFound();
            }
            return Ok(heroes);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Heroes>> GetHeroById(string id)
        {
            var hero = await _heroRepository.GetHeroById(id);
            if (hero == null)
            {
                return NotFound();
            }
            return Ok(hero);
        }
        [HttpGet("suggest")]
        public async Task<ActionResult<IEnumerable<Heroes>>> GetSuggestion( string suggestion)
        {
            var suggest = await _heroRepository.GetSuggestion(suggestion);
            if (!suggest.Any())
            {
                return NotFound();
            }
            return Ok(suggest);
        }

    }
}
