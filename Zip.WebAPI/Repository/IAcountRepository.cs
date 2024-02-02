using System.Collections.Generic;
using System.Threading.Tasks;
using Zip.WebAPI.Models;
using Zip.WebAPI.Models.Dto;

namespace Zip.WebAPI.Repository
{
    public interface IAcountRepository
    {
        Task<Acount> CreateAsync(Acount acount );
      //  Task DeleteAsync(int id, strin);
        Task<List<Acount>> GetAcountsByUserIdAsync(int userId);

    }
}
