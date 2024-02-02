using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.WebAPI.Models.Dto;
using Zip.WebAPI.Models.Responses;

namespace Zip.WebAPI.Services
{
    public interface IAcountService
    {
        Task<Response<List<AcountDto>>> GetAcountsByUserIdAsync(int userId);

        Task<Response<AcountDto>> CreateAcountAsync(AcountDto acount);
    }
}
