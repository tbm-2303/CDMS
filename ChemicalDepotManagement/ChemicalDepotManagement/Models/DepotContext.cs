using Microsoft.EntityFrameworkCore;

namespace ChemicalDepotManagement.Models
{
    public class DepotContext : DbContext
    {
        public DepotContext(DbContextOptions<DepotContext> options) : base(options) { }

        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Chemical> Chemicals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring relationships
            modelBuilder.Entity<Chemical>()
                .HasOne(c => c.Job)          // A chemical has one job
                .WithMany(j => j.Chemicals) // A job has many chemicals
                .HasForeignKey(c => c.JobId); // Foreign key for Chemical

            modelBuilder.Entity<Chemical>()
                .HasOne(c => c.Warehouse)   // A chemical has one warehouse
                .WithMany(w => w.Chemicals) // A warehouse has many chemicals
                .HasForeignKey(c => c.WarehouseId); // Foreign key for Warehouse
        }
    }
}
