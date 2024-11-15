using HeroesApi.Interfaces;
using HeroesApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HeroesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroRepository _heroRepository;
        private readonly IMemoryCache _cache;

        public HeroesController(IHeroRepository heroRepository, IMemoryCache cache)
        {
            _heroRepository = heroRepository;
            _cache = cache; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Heroes>>> GetAll()
        {
          var heroes =  await _heroRepository.GetAll();
            if (heroes == null) {
                NotFound();
            }
            return Ok(heroes);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Heroes>> GetById(string id)
        {            
            if (!_cache.TryGetValue($"hero-{id}", out var hero)) { 
            
                hero = await _heroRepository.GetById(id);

                //15 minutes for getting data from cache
                _cache.Set($"hero-{id}", hero, TimeSpan.FromMinutes(15));
            }               
                         
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

        [HttpPost]
        public async Task<ActionResult> Add(Heroes hero)
        {
             _heroRepository.Add(hero);
            await _heroRepository.SaveChangesAsync();
            return Created();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return BadRequest("Id is required");
            }

            await _heroRepository.Delete(id);
            await _heroRepository.SaveChangesAsync();

            _cache.Remove($"hero-{id}");
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id,Heroes hero)
        {
            await _heroRepository.Update(id, hero); 
            await _heroRepository.SaveChangesAsync();
            _cache.Remove($"hero-{id}");
            return NoContent();
        }   
    }
}
