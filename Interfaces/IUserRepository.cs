using HeroesApi.Models;

namespace HeroesApi.Interfaces
{
    public interface IUserRepository
    {
        Task Login(string username, string password);
        Task Logout( string username);

        Task Register(Users user);


    }
}
