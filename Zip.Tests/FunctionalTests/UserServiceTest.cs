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
        [DynamicData(nameof(ApplyDateForUserIdsTestCases))]
        public async Task GetUserByIdAsync_When_Found_ReturnUserSuccessFulResponce(int id)
        {
            //Arrange
            _userRepository.ConfigureGetUserByIdAsyncToReturnUser(id);

            //Act
            var result = await UserService.GetUserByIdAsync(id);

            //Assert
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(id, result.Data.Id);

        }

        [TestMethod]
        [DynamicData(nameof(ApplyDateForNotExistUserIdsTestCases))]
        public async Task GetUserByIdAsync_When_NotFound_ReturnNotUnsuccessfulresponce(int id)
        {
            //Arrange
            _userRepository.ConfigureGetUserByIdAsyncToReturnUser(id);

            //Act
            var result = await UserService.GetUserByIdAsync(id);

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
            Assert.AreEqual(userDto.Id, result.Data.Id);
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


        private static IEnumerable<object[]> ApplyDateForUserIdsTestCases
        {
            get
            {
                return new[]
                {
                    new object[] { 1},
                    new object[] { 4}
                };
            }
        }

        private static IEnumerable<object[]> ApplyDateForNotExistUserIdsTestCases
        {
            get
            {
                return new[]
                {
                    new object[] { 0},
                    new object[] { 20}
                };
            }
        }

        private static IEnumerable<object[]> ApplyDataForCorrectUserTestCases
        {
            get
            {
                return new[]
                {
                   new object[]{  new UserDto { Id = 5, Name = "TestUser5", Email = "testuser5@zip.com", Salary = 5000, Expenses = 3000 } },
                   new object[]{ new UserDto { Id = 10, Name = "TestUser10", Email = "testuser10@zip.com", Salary = 3000, Expenses = 500 } }
                };
            }
        }

        private static IEnumerable<object> ApplyDataForCreateUsersExistingEmailTestCases
        {
            get
            {
                return new[]
                {
                     new object[]{new UserDto{Id=5,Name="TestUser5",Email="testuser1@zip.com",Salary=7000,Expenses=3000} },
                     new object[]{new UserDto{Id=10,Name="TestUser10",Email="testuser4@zip.com",Salary=9000,Expenses=500} }
                };
            }
        }
        //private static IEnumerable<object[]> ApplyDataForPassengersNoNameTestCases
        //{
        //    get
        //    {
        //        return new[]
        //        {
        //            new object[] { 3, null },
        //            new object[] { 2, null },
        //            new object[] { 5, null }
        //        };
        //    }
        //}

        //private static IEnumerable<object[]> ApplyDataForPassengersAndNameTestCases
        //{
        //    get
        //    {
        //        return new[]
        //        {
        //            new object[] { 3, "sedan" },
        //            new object[] { 2, "hatchBack" },
        //            new object[] { 5, "suv" }
        //        };
        //    }
        //}

        //private static IEnumerable<object[]> ApplyDataForThreePassengersAndNameSedanTestCases
        //{
        //    get
        //    {
        //        return new[]
        //        {
        //            new object[] { 3, "sedan" }
        //        };
        //    }
        //}
    }
}
