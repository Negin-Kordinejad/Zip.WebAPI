using Moq;
using Zip.WebAPI.Models.Responses;
using Zip.WebAPI.Services;

namespace Zip.Tests.Fakes
{
    internal static class CrediValidatorFake
    {
        internal static Mock<ICreditValidator> ConfigureValidateAsyncToReturnRespoce(
            this Mock<ICreditValidator> instance, bool response)
        {
            instance.Setup(x => x.ValidateAsync(It.IsAny<int>()))
                .ReturnsAsync(() => new ValidateUserCreditResponseData { IsValid = response });

            return instance;
        }
    }
}
