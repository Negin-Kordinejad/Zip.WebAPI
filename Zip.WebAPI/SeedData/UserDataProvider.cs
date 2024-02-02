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
                    new() { Id=1, Name="user1",Email="e1@gmail.com",Salary=5000 },
                    new() { Id=2, Name="user2",Email="e2@gmail.com",Salary=7000 },
                    new() { Id=3, Name="user3",Email="e3@gmail.com",Salary=9000 }
            };
        }
    }
}
