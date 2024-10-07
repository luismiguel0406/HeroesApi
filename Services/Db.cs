using Microsoft.EntityFrameworkCore;
using PokeApi.Models;

namespace PokeApi.Services
{
    public static class Db
    {

        public static  IServiceCollection DbConecctionService (this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddDbContext<DcComicsDb>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DcComicsDb"))

                );
            return services;
        }
    }
}
