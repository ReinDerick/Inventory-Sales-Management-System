using InventoryManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;


namespace InventoryManagement.Data
{
    public class InventoryManagementDbContext : DbContext
    {
        public InventoryManagementDbContext(DbContextOptions<InventoryManagementDbContext> options) : base(options)
        {
        }

        public DbSet<ProductManagement> productManagement { get; set; }
    }
}
