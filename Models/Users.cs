namespace HeroesApi.Models
{
    public class Users
    {
        public int ?Id { get; set; }

        public required string Username { get; set; }

        public  string ?Email { get; set; }

        public  string ?Password { get; set; }

        public bool IsActive { get; set; }
        public bool ?IsLogged { get; set; }       

        public Users()
        {
            IsActive = true;
        }
    }
}
