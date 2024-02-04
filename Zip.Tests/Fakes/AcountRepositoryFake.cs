using Moq;
using System.Linq;
using Zip.Tests.Fixtures;
using Zip.WebAPI.Models;
using Zip.WebAPI.Repository;

namespace Zip.Tests.Fakes
{
    internal static class AcountRepositoryFake
    {
        internal static Mock<IAcountRepository> ConfigureCreateAsyncToReturnAcount(
            this Mock<IAcountRepository> instance, Acount acount)
        {
            instance.Setup(x => x.CreateAsync(It.IsAny<Acount>()))
                .ReturnsAsync(() => AcountFixtures.AddAcount(acount));

            return instance;
        }

        internal static Mock<IAcountRepository> ConfigureGetUsersAsyncToReturnUsers(
           this Mock<IAcountRepository> instance, string email)
        {
            instance.Setup(x => x.GetByUserEmailAsync(It.Is<string>(u => u.ToLower() == email.ToLower())))
                .ReturnsAsync(() => AcountFixtures.GetAcountsByUserId(email));

            return instance;
        }

    }
}
