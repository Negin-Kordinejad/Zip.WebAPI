using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Repository
{
    public interface IAcountRepository
    {
        Task<Acount> CreateAsync(Acount acount );
        Task<List<Acount>> GetByUserEmailAsync(string email);

    }
}
