using ChemicalDepotManagement.Models.ChemicalDepotManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ChemicalDepotManagement.Models
{
    public class DepotContext : DbContext
    {
        public DepotContext(DbContextOptions<DepotContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; } // DbSet for Tickets
        public DbSet<Job> Jobs { get; set; } // DbSet for Jobs
        public DbSet<Chemical> Chemicals { get; set; } // DbSet for Chemicals

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships if needed
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Job)
                .WithOne()
                .HasForeignKey<Ticket>(t => t.JobId);

            modelBuilder.Entity<Job>()
                .HasMany(j => j.Chemicals)
                .WithOne(c => c.Job)
                .HasForeignKey(c => c.JobId);
        }
    }
}

