using Microsoft.EntityFrameworkCore;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Data
{
    public interface IZipUserDBContext
    {
        DbSet<Acount> Acounts { get; set; }
        DbSet<User> Users { get; set; }
    }
}