using AutoFixture;
using System.Collections.Generic;
using System.Linq;
using Zip.WebAPI.Models;
using Zip.WebAPI.Models.Enum;

namespace Zip.Tests.Fixtures
{
    internal static class AcountFixtures
    {
        internal static Acount AcountTypeAForUser3 = GetFixture().Build<Acount>()
        .With(x => x.Id, 1)
        .With(x => x.UserId, 3)
        .With(x => x.Type, AcountType.Credit.ToString())
        .Create();

        internal static Acount AcountTypeAForUser4 = GetFixture().Build<Acount>()
        .With(x => x.Id, 2)
        .With(x => x.UserId, 4)
        .With(x => x.Type, AcountType.Credit.ToString())
        .Create();

        internal static Acount AcountTypeBForUser4 = GetFixture().Build<Acount>()
        .With(x => x.Id, 3)
        .With(x => x.UserId, 4)
        .With(x => x.Type, AcountType.Choice.ToString())
        .Create();

        private static List<Acount> acounts => new() { AcountTypeAForUser3, AcountTypeAForUser4, AcountTypeBForUser4 };

        public static Acount AddAcount(Acount acount)
        {
            if (acount != null)
            {
                acounts.Add(acount);
            }
            return acount;
        }

        internal static List<Acount> GetAcountsByUserId(string email)
        {
            return acounts.Where(u => u.User.Email.ToLower() == email.ToLower()).ToList();
        }
        private static Fixture GetFixture()
        {
            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b)); fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            return fixture;
        }
    }
}
