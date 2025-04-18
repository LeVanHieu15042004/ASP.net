using levanhieu_2122110522.Model;
using Microsoft.EntityFrameworkCore;

namespace levanhieu_2122110522.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
