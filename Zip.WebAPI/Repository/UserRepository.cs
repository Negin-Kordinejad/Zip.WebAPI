using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _zipUserDBContext.Users.Where(user => user.Id == id)
                                                    .FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _zipUserDBContext.Users.Where(user => user.Email.ToLower() == email.ToLower())
                                                .Include(_ => _.Acounts)
                                                .FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _zipUserDBContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            _zipUserDBContext.Users.Add(user);
            await _zipUserDBContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            var userToUpdate = await _zipUserDBContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == user.Email.ToLower());
            if (userToUpdate != null)
            {
                userToUpdate.Name = user.Name;
                userToUpdate.Salary = user.Salary;
                userToUpdate.Expenses = user.Expenses;
            }
            await _zipUserDBContext.SaveChangesAsync();

            return userToUpdate;
        }

        public async Task DeleteAsync(string email)
        {
            var user = _zipUserDBContext.Users.Where(u => u.Email.ToLower() == email.ToLower())
                                              .FirstOrDefault();
            if (user != null)
            {
                _zipUserDBContext.Users.Remove(user);

                await _zipUserDBContext.SaveChangesAsync();
            }
        }
    }
}
