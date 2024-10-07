namespace HeroesApi.Models
{
    public class Users
    {
        public int Id { get; set; }

        public required string User { get; set; }

        public required string Email { get; set; }
    }
}
