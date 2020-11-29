using SalesApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace SalesApp.Database
{
    public class SQLiteDbService : ILocalDbService
    {
        private SQLiteConnection _database;

        public void CreateUser(User user)
        {
            _database.Insert(user);
        }

        public void DeleteUser()
        {
            _database.Execute("DELETE FROM User");
        }

        public User GetUser()
        {
            return _database.Table<User>().FirstOrDefault();
        }

        public void Initialize()
        {
            if (_database == null)
            {
                var dbPath = Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData),"SalesAppDb.db3"   
                    );

                _database = new SQLiteConnection(dbPath);
            }

            _database.CreateTable<User>();
            _database.CreateTable<Address>();
        }

        public List<Address> GetAllAddresses()
        {
            return _database.Table<Address>().ToList();
        }

        public void UpsertAddress(Address address)
        {
            _database.InsertOrReplace(address);
        }
    }
}
