using Microsoft.AspNetCore.Mvc;
using PokeApi.Interfaces;
using PokeApi.Services;

namespace PokeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementController : ControllerBase
    {
        private readonly IElement _elementTeller;
        public ElementController(IElement elementTeller)
        {
            _elementTeller = elementTeller;
        }

        [HttpGet("write-element/{message?}")]
        //If I had to use ActionResult instead IActionResult, should specify return type. Example: ActionResult<String>
        public IActionResult GetWriteElement(string message)
        {
            if (string.IsNullOrEmpty(message))
             {
                return NoContent() ;
              }
            return Ok(_elementTeller.WriteElement($"Test from browser with message: {message}"));
                
        }

        [HttpGet("write-element-delayed")]
        public async Task<string> GetWriteElementWithDelay()
        {
            await Task.Delay(2000);
            return _elementTeller.WriteElement("Test from browser 3G");
        }

        // GET: api/<ElementController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return ["value1", "value2" ];
        }

        // GET api/<ElementController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ElementController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ElementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ElementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
