using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAsync(string email);
        Task<List<User>> GetAllAsync();
        Task<User> CreateAsync(User user );
        Task DeleteAsync(string id);
    }
}
