using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zip.WebAPI.Data;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ZipUserDBContext _zipUserDBContext;

        public UserRepository(ZipUserDBContext zipUserDBContext)
        {
            _zipUserDBContext = zipUserDBContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            _zipUserDBContext.Users.Add(user);
            await _zipUserDBContext.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(int id)
        {
            var user = _zipUserDBContext.Users.Where(u => u.Id == id).FirstOrDefault();
            if (user != null)
            {
                _zipUserDBContext.Users.Remove(user);
                await _zipUserDBContext.SaveChangesAsync();
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user =await _zipUserDBContext.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetByUserByEmailAsync(string Email)
        {
            return await _zipUserDBContext.Users.Where(user => user.Email == Email)
                .Include(_=>_.Acounts).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _zipUserDBContext.Users.ToListAsync();
        }
    }
}
