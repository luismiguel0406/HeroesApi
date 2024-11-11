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
        public async Task<Users?> Login(Users user)
        {
             return await _context.Users
            .Where(u => u.Username == user.Username && u.Password == user.Password)
            .FirstOrDefaultAsync();       
        }

        public Task Logout(string username)
        {
            throw new NotImplementedException();
        }

        public async Task Register(Users user)
        {
            var ExistUser = await _context.Users.AnyAsync(u=>u.Username == user.Username);
            if (ExistUser) return;
               _context.Users.Add(user);
            
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
