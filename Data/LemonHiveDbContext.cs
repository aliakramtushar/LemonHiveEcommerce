using LemonHiveEcommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace LemonHiveEcommerce.Data
{
    public class LemonHiveDbContext : DbContext
    {
        public LemonHiveDbContext(DbContextOptions<LemonHiveDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
