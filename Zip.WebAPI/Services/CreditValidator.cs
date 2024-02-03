using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Zip.WebAPI.Models.Dto;
using Zip.WebAPI.Models.Responses;

namespace Zip.WebAPI.Services
{
    public class CreditValidator : ICreditValidator
    {
        private const int Zip_USER_CREDIT_TO_HAVE_ACOUNT= 1000;
        private readonly ILogger<CreditValidator> _logger;
        private readonly IUserService _userService;

        public CreditValidator(ILogger<CreditValidator> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<ValidateUserCreditResponseData> ValidateAsync(int userId)
        {
            var result = new ValidateUserCreditResponseData { IsValid = false };
            _logger.LogInformation("CreditValidator_ValidateAsync attemting....");

            var response = await _userService.GetUserByIdAsync(userId);
            if (response == null || !response.IsSuccessful || !HashCredit(response.Data))
            {
                result.IsValid = false;
                return result;
            }

            result.IsValid = true;

            return result;

        }

        private bool HashCredit(UserDto user)
        {
            return ((user.Salary - user.Expenses) > Zip_USER_CREDIT_TO_HAVE_ACOUNT);
        }
    }
}
