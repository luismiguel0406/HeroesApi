using HeroesApi.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace HeroesApi.Controllers
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
                return NoContent();
            }
            return Ok(_elementTeller.WriteElement($"Test from browser with message: {message}"));

        }

        [HttpGet("write-element-delayed")]
        public async Task<string> GetWriteElementWithDelay()
        {
            await Task.Delay(2000);
            return _elementTeller.WriteElement("Test from browser 3G");
        }

     
       
    }
}
