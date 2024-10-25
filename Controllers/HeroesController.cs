using HeroesApi.Interfaces;
using HeroesApi.Models;
using Microsoft.AspNetCore.Mvc;

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
            var hero = await _heroRepository.GetById(id);
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
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id,Heroes hero)
        {
            await _heroRepository.Update(id, hero); 
            await _heroRepository.SaveChangesAsync();
            return NoContent();
        }   
    }
}
