using HeroesApi.Models;

namespace HeroesApi.Interfaces
{
    public interface ITokenGererator
    {
        string GenerateToken(Users user, WebApplicationBuilder builder);
    }
}
