using HeroesApi.Models;

namespace HeroesApi.Interfaces
{
    public interface IUserRepository
    {
        Task<Users?> Login(Users user);
        Task Logout( string username);

        Task Register(Users user);

        Task SaveChangesAsync();

    }
}
