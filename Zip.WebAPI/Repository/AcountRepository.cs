using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zip.WebAPI.Data;
using Zip.WebAPI.Models;
using Zip.WebAPI.Models.Dto;

namespace Zip.WebAPI.Repository
{
    public class AcountRepository : IAcountRepository
    {
        private readonly ZipUserDBContext _zipUserDBContext;

        public AcountRepository(ZipUserDBContext zipUserDBContext)
        {
            _zipUserDBContext = zipUserDBContext;
        }

        public async Task<Acount> CreateAsync(Acount acount)
        {
            _zipUserDBContext.Acounts.Add(acount);
            await _zipUserDBContext.SaveChangesAsync();
            return acount;
        }

        public async Task<List<Acount>> GetByUserEmailAsync(string email)
        {
            return await _zipUserDBContext.Acounts.Where(a => a.User.Email.ToLower() == email.ToLower()).ToListAsync();
        }
    }
}
