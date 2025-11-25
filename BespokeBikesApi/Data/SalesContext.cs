using Microsoft.EntityFrameworkCore;
using BespokeBikesApi.Data.Models;

namespace BespokeBikesApi.Data
{
    public class SalesContext :  DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Salesperson> Salespersons { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {
        }
    }
}