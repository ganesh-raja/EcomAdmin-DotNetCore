using EcomAdmin.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomAdmin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Login> adminlogin { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }


    }
}
