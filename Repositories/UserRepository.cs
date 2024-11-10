using HeroesApi.Interfaces;
using HeroesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroesApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DcComicsDb _context;
        public UserRepository( DcComicsDb context)
        {
            _context = context;
        }
        public async Task Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task Logout(string username)
        {
            throw new NotImplementedException();
        }

        public async Task Register(Users user)
        {
            var ExistUser = await _context.Users.AllAsync(u=>u.Username == user.Username);
            if (ExistUser) return;
               _context.Users.Add(user);
            
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
