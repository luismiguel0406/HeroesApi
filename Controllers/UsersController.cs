using HeroesApi.Interfaces;
using HeroesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HeroesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IConfiguration _configuration;

        public UsersController(
            IUserRepository userRepository,
            ITokenGenerator tokenGenerator, 
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }
        [HttpPost]
        public async Task<ActionResult> Register(Users user)
        {
           await _userRepository.Register(user);
            var httpContext = HttpContext;
            httpContext.Response.Headers.Authorization = _tokenGenerator.GenerateToken(user, _configuration);
            return Created();
          
        }
    }
}
