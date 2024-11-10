using HeroesApi.Models;

namespace HeroesApi.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(Users user, IConfiguration configuration);
    }
}
