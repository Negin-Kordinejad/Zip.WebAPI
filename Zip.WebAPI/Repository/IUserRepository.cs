using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUserByEmailAsync(string Email);
        Task<List<User>> GetUsersAsync();
        Task<User> CreateAsync(User user );
        Task DeleteAsync(int id);

    }
}
