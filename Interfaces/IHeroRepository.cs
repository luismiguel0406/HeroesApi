using HeroesApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace HeroesApi.Interfaces
{
    public interface IHeroRepository
    {
        Task<IEnumerable<Heroes>> GetHeroes();

        Task<Heroes> GetHeroById(string id);

        Task<IEnumerable<Heroes>> GetSuggestion( string suggestion);
    }
}
