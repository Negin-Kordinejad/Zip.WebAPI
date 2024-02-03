using System.Collections.Generic;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.SeedData
{
    public static class UserDataProvider
    {
        public static List<User> Get()
        {
            return new List<User>
            {
                    new() { Name= "user1", Email= "e1@gmail.com", Salary = 5000, Expenses = 2000},
                    new() { Name= "user2", Email= "e2@gmail.com", Salary = 7000 ,Expenses = 6000},
                    new() { Name= "user3", Email= "e3@gmail.com", Salary = 9000 ,Expenses = 10000}
            };
        }
    }
}
