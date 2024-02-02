using Microsoft.EntityFrameworkCore;
using Zip.WebAPI.Models;

namespace Zip.WebAPI.Data
{
    public class ZipUserDBContext : DbContext, IZipUserDBContext
    {
        public ZipUserDBContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=:memory:", x => { });
            // optionsBuilder.UseInMemoryDatabase("ZipAcount");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Acount> Acounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => new { e.Id });
                entity.Property(p => p.Name).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique(true);
                entity.Property(p => p.Salary).IsRequired();
                entity.Property(p => p.Expenses).IsRequired();
            }
            );
            modelBuilder.Entity<Acount>(entity =>
            {
                entity.ToTable("Acount");
                entity.HasOne(n => n.User).WithMany(m => m.Acounts).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasKey(e => new { e.Id });
                entity.Property(p => p.Type).IsRequired();
            }
            );
        }

    }
}
