using SalesApp.Models;
using System.Collections.Generic;

namespace SalesApp.Database
{
    public interface ILocalDbService
    {
        void Initialize();

        void CreateUser(User user);
        User GetUser();
        void DeleteUser();

        void UpsertAddress(Address address);
        List<Address> GetAllAddresses();
    }
}
