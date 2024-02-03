using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
        [DynamicData(nameof(ApplyDateForAcountWithCreditTestCases))]
        public async Task CreateAcountAsync_WHEN_USER_HAS_CREDIT_ReturnSuccessFulResponce(AcountDto acountDto)
        {
            //Arrange
            var acount = Mapper.Map<Acount>(acountDto);
            _acountRepository.ConfigureCreateAsyncToReturnAcount(acount);
            _creditValidator.ConfigureValidateAsyncToReturnRespoce(true);
            //Act
            var result = await AcountService.CreateAcountAsync(acountDto);

            //Assert
            Assert.IsTrue(result.IsSuccessful);
            Assert.AreEqual(acountDto.UserId, result.Data.UserId);

        }

        [TestMethod]
        [DynamicData(nameof(ApplyDateForAcountWithNoCreditTestCases))]
        public async Task CreateAcountAsync_WHEN_USER_HAS_NO_CREDIT_ReturnUnSuccessFuk(AcountDto acountDto)
        {
            //Arrange
            var acount = Mapper.Map<Acount>(acountDto);
            _acountRepository.ConfigureCreateAsyncToReturnAcount(acount);
            _creditValidator.ConfigureValidateAsyncToReturnRespoce(false);
            //Act
            var result = await AcountService.CreateAcountAsync(acountDto);

            //Assert
            Assert.IsFalse(result.IsSuccessful);
        }


        private static IEnumerable<object[]> ApplyDateForAcountWithCreditTestCases
        {
            get
            {
                return new[]
                {
                   new object[]{ new AcountDto { UserId = UserFixtures.CreditUserId2NoAcount.Id, Type = AcountType.Credit.ToString() } },
                };
            }
        }
        private static IEnumerable<object[]> ApplyDateForAcountWithNoCreditTestCases
        {
            get
            {
                return new[]
                {
                    new object[]{ new AcountDto{ UserId=UserFixtures.NoCreditUserId1NoAcount.Id,Type= AcountType.Choice.ToString()} },
                };
            }
        }


    }
}
