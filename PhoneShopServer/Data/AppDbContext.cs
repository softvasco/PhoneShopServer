using Microsoft.EntityFrameworkCore;
using PhoneShopSharedLibrary.Models;

namespace PhoneShopServer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = default!;

    }
}
