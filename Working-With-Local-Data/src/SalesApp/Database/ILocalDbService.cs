using SalesApp.Models;

namespace SalesApp.Database
{
    public interface ILocalDbService
    {
        void Initialize();

        void CreateUser(User user);
        User GetUser();
        void DeleteUser();
    }
}
