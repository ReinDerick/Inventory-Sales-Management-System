using InventoryManagement.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;


namespace InventoryManagement.Data
{
    public class InventoryManagementDbContext : DbContext
    {
        internal object userAccount;

        public InventoryManagementDbContext(DbContextOptions<InventoryManagementDbContext> options) : base(options)
        {
        }

        public DbSet<ProductManagement> productManagement { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<userAccount> UserAccounts { get; set; }

        public DbSet<userRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the relationship between userAccount and userRole
            modelBuilder.Entity<userRole>()
                .HasOne(ur => ur.UserAccount)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            // Configure the relationship between Role and userRole
            modelBuilder.Entity<userRole>()
                .HasOne(ur => ur.Roles)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}
