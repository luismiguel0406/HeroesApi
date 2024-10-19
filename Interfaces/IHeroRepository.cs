using HeroesApi.Models;

namespace HeroesApi.Interfaces
{
    public interface IHeroRepository
    {
        Task<IEnumerable<Heroes>> GetAll();

        Task<Heroes> GetById(string id);

        Task<IEnumerable<Heroes>> GetSuggestion( string suggestion);

        void Add(Heroes heroes);

        void Update(Heroes heroes);

        void Delete(string id);  
        
        Task SaveChangesAsync();
    }
}
