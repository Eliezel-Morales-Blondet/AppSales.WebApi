using AppSales.WebApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace AppSales.WebApi.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Sale> Sale { get; set; }
        public DbSet<SaleDetail> SaleDetail { get; set; }

    }
}
