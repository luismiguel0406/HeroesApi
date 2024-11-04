namespace HeroesApi.Interfaces
{
    public interface IUser
    {
        void Login(string username, string password);
        void Logout( string username);

        void Register(string username, string password);


    }
}
