using levanhieu_2122110522.Model;
using Microsoft.EntityFrameworkCore;

namespace levanhieu_2122110522.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
