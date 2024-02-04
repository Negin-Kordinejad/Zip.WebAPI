using Moq;
using System.Linq;
using Zip.Tests.Fixtures;
using Zip.WebAPI.Models;
using Zip.WebAPI.Repository;

namespace Zip.Tests.Fakes
{
    internal static class UserRepositoryFake
    {
        internal static Mock<IUserRepository> ConfigureGetUserByEmailAsyncToReturnUser(
            this Mock<IUserRepository> instance, string email)
        {
            instance.Setup(x => x.GetByEmailAsync(It.Is<string>(p => p == email)))
                .ReturnsAsync(() => UserFixtures.Users.FirstOrDefault(user => user.Email.ToLower() == email.ToLower()));

            return instance;
        }

        internal static Mock<IUserRepository> ConfigureGetUsersAsyncToReturnUsers(
           this Mock<IUserRepository> instance)
        {
            instance.Setup(x => x.GetAllAsync())
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
