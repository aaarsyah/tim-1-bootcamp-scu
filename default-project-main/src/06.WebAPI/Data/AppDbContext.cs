using Microsoft.EntityFrameworkCore;
using ProductApel.Models; // Ganti sesuai namespace model kamu

namespace ProductApel.Data // Pastikan namespace ini benar
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
