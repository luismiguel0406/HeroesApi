namespace HeroesApi.Models
{
    public class Users
    {
        public int ?Id { get; set; }

        public required string Username { get; set; }

        public required string Email { get; set; }

        public  string ?Password { get; set; }
    }
}
