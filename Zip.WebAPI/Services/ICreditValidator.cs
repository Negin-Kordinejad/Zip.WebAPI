using System.Threading.Tasks;
using Zip.WebAPI.Models;
using Zip.WebAPI.Models.Responses;

namespace Zip.WebAPI.Services
{
    public interface ICreditValidator
    {
        Task<ValidateUserCreditResponseData> ValidateAsync(User user);
    }
}
