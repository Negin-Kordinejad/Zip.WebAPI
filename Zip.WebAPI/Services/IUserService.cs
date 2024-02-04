using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.WebAPI.Models.Dto;
using Zip.WebAPI.Models.Responses;

namespace Zip.WebAPI.Services
{
    public interface IUserService
    {
        Task<Response<UserAcountDto>> GetUserByEmailAsync(string email);
        Task<Response<List<UserDto>>> GetUsersAsync();
        Task<Response<UserDto>> CreateUserAsync(UserDto user);
        Task DeleteUserAsync(string email);
    }
}
