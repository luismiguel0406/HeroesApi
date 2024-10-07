using System.ComponentModel.DataAnnotations;

namespace PokeApi.Models
{
    public class Heroes
    {
        public required string Id { get; set; }   
        public required string Superhero { get; set; }
        public required string Publisher { get; set; }
        public required string AlterEgo { get; set; }
        public required string FirstAppearance { get; set; }
        public required string Characters { get; set; }
    }
}
