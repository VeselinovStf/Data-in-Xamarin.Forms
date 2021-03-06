﻿using System.Threading.Tasks;
using SalesApp.Models;

namespace SalesApp.Services
{
    public class MockUserService : IUserService
    {
        public async Task<User> GetUser(string token)
        {
            //await Task.Delay(1000);
            //Changing starter implementation to look more real
            var user =  new User()
            {
                Email = "john@test.com",
                FirstName = "John",
                LastName = "Smith",
                Token = "38259836-F79C-4913-BC20-B5B9457E35FE",
                AgentID = "2452524"
            };

            if (user.Token.Equals(token))
            {
                return user;
            }

            return null;
        }
    }
}