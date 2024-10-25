namespace HeroesApi.Models
{
    public class Heroes
    {
        public  string ?Id { get; set; }
        public  string ?Superhero { get; set; }
        public  string ?Publisher { get; set; }
        public  string ?AlterEgo { get; set; }
        public  string ?FirstAppearance { get; set; }
        public  string ?Characters { get; set; }
        public  string ?ImageUrl  { get; set; }
        public  bool IsActive { get; set; }

        public Heroes()
        {
            IsActive = true;
        }
    }
}
