using AutoFixture;
using System.Collections.Generic;
using System.Linq;
using Zip.WebAPI.Models;


namespace Zip.Tests.Fixtures
{
    internal static class UserFixtures
    {
        internal static User NoCreditUserId1NoAcount = GetFixture().Build<User>()
        .With(x => x.Id, 1)
        .With(x => x.Name, "TestUser1")
        .With(x => x.Email, "tuser1@zip.com")
        .With(x => x.Salary, 1000)
        .With(x => x.Expenses, 400)
        .Without(x => x.Acounts)
        .Create();

        internal static User CreditUserId2NoAcount = GetFixture().Build<User>()
       .With(x => x.Id, 2)
       .With(x => x.Name, "TestUser2")
       .With(x => x.Email, "tuser2@zip.com")
       .With(x => x.Salary, 5000)
       .With(x => x.Expenses, 1000)
       .Create();

        internal static User UserId3WithAcount = GetFixture().Build<User>()
        .With(x => x.Id, 3)
        .With(x => x.Name, "TestUser3")
        .With(x => x.Email, "tuser3@zip.com")
        .With(x => x.Salary, 6000)
        .With(x => x.Expenses, 4000)
        .With(x => x.Acounts, () => new List<Acount> { AcountFixtures.AcountTypeAForUser3 })
       .Create();

        internal static User UserId4WithAcount = GetFixture().Build<User>()
        .With(x => x.Id, 4)
        .With(x => x.Name, "TestUser4")
        .With(x => x.Email, "tuser4@zip.com")
        .With(x => x.Salary, 1000)
        .With(x => x.Expenses, 400)
        .With(x => x.Acounts, () => new List<Acount> { AcountFixtures.AcountTypeAForUser3, AcountFixtures.AcountTypeBForUser4 })
       .Create();

        private static List<User> users => new() { NoCreditUserId1NoAcount, CreditUserId2NoAcount, UserId3WithAcount, UserId4WithAcount };

        public static List<User> Users
        {
            get { return users; }
            private set { }
        }

        public static User AddUser(User user)
        {
            if (user == null || users.Any(u => u.Email.ToLower() == user.Email.ToLower()))
            {
                return null;
            }
            users.Add(user);
            return user;
        }

        public static void RemoveUser(int userId)
        {
            var deleteUser = users.Where(u => u.Id == userId).FirstOrDefault();
            if (deleteUser != null)
            {
                users.Remove(deleteUser);
            }
        }
        private static Fixture GetFixture()
        {
            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b)); fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return fixture;
        }
    }
}
