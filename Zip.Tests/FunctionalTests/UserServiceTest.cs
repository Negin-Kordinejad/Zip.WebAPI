using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.Tests.Fakes;
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
    }
}
