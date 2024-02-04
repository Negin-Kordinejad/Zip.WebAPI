using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zip.Tests.Fakes;
using Zip.Tests.Fixtures;
using Zip.WebAPI.Models;
using Zip.WebAPI.Models.Dto;
using Zip.WebAPI.Models.Enum;

namespace Zip.Tests.Functional
{
    [TestClass]
    public class AcountServiceTest : FunctionalTestBase
    {
        [TestMethod]
        [DynamicData(nameof(ApplyDataForAcountWithCreditTestCases))]
        public async Task CreateAcountAsync_WHEN_USER_HAS_CREDIT_ReturnSuccessFulResponce(AcountCreateDto acountDto)
        {
            //Arrange
            var acount = new Acount { UserId = UserFixtures.Users.FirstOrDefault(u => u.Email == acountDto.Email).Id, Type = acountDto.Type };
            _acountRepository.ConfigureCreateAsyncToReturnAcount(acount);
            _userRepository.ConfigureGetUserByEmailAsyncToReturnUser(acountDto.Email);
            _creditValidator.ConfigureValidateAsyncToReturnRespoce(true);
            //Act
            var result = await AcountService.CreateAcountAsync(acountDto);

            //Assert
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(acountDto.Type, result.Data.Type);
        }

        [TestMethod]
        [DynamicData(nameof(ApplyDataForAcountWithNoCreditTestCases))]
        public async Task CreateAcountAsync_WHEN_USER_HAS_NO_CREDIT_ReturnUnSuccessFuk(AcountCreateDto acountDto)
        {
            //Arrange
            var acount = new Acount { UserId = UserFixtures.Users.FirstOrDefault(u => u.Email == acountDto.Email).Id, Type = acountDto.Type };
            _acountRepository.ConfigureCreateAsyncToReturnAcount(acount);
            _userRepository.ConfigureGetUserByEmailAsyncToReturnUser(acountDto.Email);
            _creditValidator.ConfigureValidateAsyncToReturnRespoce(false);
            //Act
            var result = await AcountService.CreateAcountAsync(acountDto);

            //Assert
            Assert.IsFalse(result.IsSuccessful);
        }

        private static IEnumerable<object[]> ApplyDataForAcountWithCreditTestCases
        {
            get
            {
                return new[]
                {
                   new object[]{ new AcountCreateDto { Email = UserFixtures.CreditUserId2NoAcount.Email, Type = AcountType.Credit.ToString() } },
                };
            }
        }
        private static IEnumerable<object[]> ApplyDataForAcountWithNoCreditTestCases
        {
            get
            {
                return new[]
                {
                    new object[]{ new AcountCreateDto{ Email= UserFixtures.NoCreditUserId1NoAcount.Email, Type= AcountType.Choice.ToString()} },
                };
            }
        }
    }
}
