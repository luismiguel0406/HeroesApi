using HeroesApi.Interfaces;
using HeroesApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeroesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
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
        [HttpPost("Register")]
        public async Task<ActionResult> Register(Users user)
        {
           await _userRepository.Register(user);
           await _userRepository.SaveChangesAsync();
            return Created();
          
        }
        [HttpPost("Login")]
        public async Task<ActionResult> Login(Users user)
        {
            var ExistUser = await _userRepository.Login(user);
            if (ExistUser == null)
            {
                return Unauthorized();
            }
            var token = _tokenGenerator.GenerateToken(ExistUser, _configuration);
            Response.Headers.Authorization = $"Bearer {token}";
            return Ok(new
            {
                token,
                message = "Logged successfully",
            });
        }
    }
}
