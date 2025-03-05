using Microsoft.EntityFrameworkCore;
using ProductApi.Models;
using System.Diagnostics;

namespace ProductApi.DataAccess
{
    public class ProductRepository : DbContext
    {
        public ProductRepository(DbContextOptions<ProductRepository> options) : base(options)
        {

        }

        public DbSet<Users> authenticateUser { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
