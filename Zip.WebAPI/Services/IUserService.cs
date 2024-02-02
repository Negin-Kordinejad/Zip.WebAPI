using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.WebAPI.Models.Dto;
using Zip.WebAPI.Models.Responses;

namespace Zip.WebAPI.Services
{
    public interface IUserService
    {
        Task<Response<UserDto>> GetUserByIdAsync(int id);
        Task<Response<List<UserDto>>> GetUsersAsync();
        Task<Response<UserDto>> CreateUserAsync(UserDto user);
        Task DeleteUserAsync(int id);
    }
}
