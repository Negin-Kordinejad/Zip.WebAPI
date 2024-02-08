using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zip.Tests.Fakes;
using Zip.Tests.Fixtures;
using Zip.WebAPI.Models;
using Zip.WebAPI.Models.Dto;

namespace Zip.Tests.Functional
{
    [TestClass]
    public class UserServiceTest : FunctionalTestBase
    {
        [TestMethod]
        [DynamicData(nameof(ApplyDateForUserEmailTestCases))]
        public async Task GetUserByEmailAsync_When_Found_ReturnUserSuccessFulResponce(string email)
        {
            //Arrange
            _userRepository.ConfigureGetUserByEmailAsyncToReturnUser(email);

            //Act
            var result = await UserService.GetUserByEmailAsync(email);

            //Assert
            Assert.IsTrue(result.IsSuccessful);
            Assert.IsNotNull(result.Data);

        }

        [TestMethod]
        [DynamicData(nameof(ApplyDateForNotExistUserEmailTestCases))]
        public async Task GetUserByIdAsync_When_NotFound_ReturnNotUnsuccessfulresponce(string email)
        {
            //Arrange
            _userRepository.ConfigureGetUserByEmailAsyncToReturnUser(email);

            //Act
            var result = await UserService.GetUserByEmailAsync(email);

            //Assert
            Assert.IsFalse(result.IsSuccessful);
        }

        [TestMethod]
        [DynamicData(nameof(ApplyDataForCorrectUserTestCases))]
        public async Task CreateUserAsync_When_User_Provide_ReturnSuccessfulUserResponce(UserDto userDto)
        {
            //Arrange
            _userRepository.ConfigureCreateAsyncToSaveAndReturnUser(Mapper.Map<User>(userDto));

            //Act
            var result = await UserService.CreateUserAsync(userDto);

            //Assert
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(userDto.Name, result.Data.Name);
            Assert.AreEqual(userDto.Email, result.Data.Email);

        }

        [TestMethod]
        [DynamicData(nameof(ApplyDataForCreateUsersExistingEmailTestCases))]
        public async Task CreateUserAsync_When_User_Existing_Email_Return_UnsuccessfulResponce(UserDto user)
        {
            //Arrange

            _userRepository.ConfigureCreateAsyncToSaveAndReturnUser(Mapper.Map<User>(user));

            //Act
            var result = await UserService.CreateUserAsync(user);

            //Assert
            Assert.IsFalse(result.IsSuccessful);

        }

        [TestMethod]
        [DynamicData(nameof(ApplyDataForUpdateUsersExistingEmailTestCases))]
        public async Task UpdateUserAsync_When_User_Existing_Return_SuccessfulUserResponce(UserDto user)
        {
            //Arrange

            _userRepository.ConfigureUpdatesyncToSaveAndReturnUser(Mapper.Map<User>(user));

            //Act
            var result = await UserService.CreateUserAsync(user);

            //Assert
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(user.Name, result.Data.Name);
            Assert.AreEqual(user.Email, result.Data.Email);
            Assert.AreEqual(user.Salary, result.Data.Salary);
            Assert.AreEqual(user.Expenses, result.Data.Expenses);

        }

        [TestMethod]
        [DynamicData(nameof(ApplyDataForUpdateUsersNoExistingEmailTestCases))]
        public async Task UpdateUserAsync_When_User_Not_Existing_Return_UnSuccessfulResponce(UserDto user)
        {
            //Arrange

            _userRepository.ConfigureUpdatesyncToSaveAndReturnUser(Mapper.Map<User>(user));

            //Act
            var result = await UserService.CreateUserAsync(user);

            //Assert
            Assert.IsFalse(result.IsSuccessful);

        }

        [Ignore]
        [TestMethod]
        [DynamicData(nameof(ApplyDataForRemoveUsersExistingEmailTestCases))]
        public async Task DeleteUserAsync_When_User_Existing_Return_SuccessfulUserResponce(string emailAddress)
        {
            //Arrange

            _userRepository.ConfigureRemovesync(emailAddress);

            //Act
            await UserService.DeleteUserAsync(emailAddress);

            //Assert
            Assert.IsFalse(UserFixtures.Users.Any(u => u.Email.ToLower() == emailAddress.ToLower()));

        }

        private static IEnumerable<object[]> ApplyDateForUserEmailTestCases
        {
            get
            {
                return new[]
                {
                    new object[] { "tuser1@zip.com"},
                    new object[] { "tuser3@zip.com"}
                };
            }
        }

        private static IEnumerable<object[]> ApplyDateForNotExistUserEmailTestCases
        {
            get
            {
                return new[]
                {
                    new object[] { "xyz@gmail.com"},
                    new object[] { "Z@zip.com"}
                };
            }
        }

        private static IEnumerable<object[]> ApplyDataForCorrectUserTestCases
        {
            get
            {
                return new[]
                {
                   new object[]{  new UserDto { Name = "TestUser5", Email = "testuser5@zip.com", Salary = 5000, Expenses = 3000 } },
                   new object[]{ new UserDto { Name = "TestUser10", Email = "testuser10@zip.com", Salary = 3000, Expenses = 500 } }
                };
            }
        }

        private static IEnumerable<object> ApplyDataForCreateUsersExistingEmailTestCases
        {
            get
            {
                return new[]
                {
                   new object[]{  new UserDto { Name = "tUser5", Email = "tuser1@zip.com", Salary = 5000, Expenses = 3000 } },
                   new object[]{ new UserDto { Name = "TestUser10", Email = "tuser3@zip.com", Salary = 3000, Expenses = 500 } }
                };
            }
        }

        private static IEnumerable<object> ApplyDataForUpdateUsersExistingEmailTestCases
        {
            get
            {
                return new[]
                {
                   new object[]{  new UserDto { Name = "Martin", Email = "tuser1@zip.com", Salary = 5000, Expenses = 3000 } },
                   new object[]{ new UserDto { Name = "TestUser10", Email = "tuser3@zip.com", Salary = 5000, Expenses = 500 } }
                };
            }
        }

        private static IEnumerable<object> ApplyDataForUpdateUsersNoExistingEmailTestCases
        {
            get
            {
                return new[]
                {
                   new object[]{  new UserDto { Name = "Martin", Email = "t@zip.com", Salary = 5000, Expenses = 3000 } },
                   new object[]{ new UserDto { Name = "TestUser10", Email = "tx@zip.com", Salary = 5000, Expenses = 500 } }
                };
            }
        }

        private static IEnumerable<object[]> ApplyDataForRemoveUsersExistingEmailTestCases
        {
            get
            {
                return new[]
                {
                    new object[] { "tuser1@zip.com"},
                    new object[] { "tuser3@zip.com" }
                };
            }
        }
    }
}
