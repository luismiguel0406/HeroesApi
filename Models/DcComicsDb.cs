using Microsoft.EntityFrameworkCore;

namespace HeroesApi.Models
{
    public class DcComicsDb : DbContext
    {
        public DcComicsDb(DbContextOptions<DcComicsDb> options) : base(options) { }

        public DbSet<Heroes> Heroes { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
