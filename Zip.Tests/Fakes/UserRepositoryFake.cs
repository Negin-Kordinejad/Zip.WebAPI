using Moq;
using System.Linq;
using Zip.Tests.Fixtures;
using Zip.WebAPI.Models;
using Zip.WebAPI.Repository;

namespace Zip.Tests.Fakes
{
    internal static class UserRepositoryFake
    {
        internal static Mock<IUserRepository> ConfigureGetUserByIdAsyncToReturnUser(
            this Mock<IUserRepository> instance, int userId)
        {
            instance.Setup(x => x.GetByIdAsync(It.Is<int>(p => p == userId)))
                .ReturnsAsync(() => UserFixtures.Users.FirstOrDefault(user => user.Id == userId));

            return instance;
        }

        internal static Mock<IUserRepository> ConfigureGetUsersAsyncToReturnUsers(
           this Mock<IUserRepository> instance)
        {
            instance.Setup(x => x.GetUsersAsync())
                .ReturnsAsync(() => UserFixtures.Users);

            return instance;
        }

        internal static Mock<IUserRepository> ConfigureCreateAsyncToSaveAndReturnUser(
         this Mock<IUserRepository> instance, User userToSave)
        {
            instance.Setup(x => x.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(() => UserFixtures.AddUser(userToSave));

            return instance;
        }

    }
}
