using HeroesApi.Interfaces;
using HeroesApi.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace HeroesApi.Services
{
    public static class MyServices
    {
        public static IServiceCollection MyCustomServices(this IServiceCollection services)
        {
            //DI
            services.AddScoped<IHeroRepository, HeroesRepository>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            return services;
        }
    }
}
