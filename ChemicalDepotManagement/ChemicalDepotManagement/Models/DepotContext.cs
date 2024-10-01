using Microsoft.EntityFrameworkCore;

namespace ChemicalDepotManagement.Models
{
    public class DepotContext : DbContext
    {
        public DepotContext(DbContextOptions<DepotContext> options) : base(options)
        {
        }

        public DbSet<Chemical> Chemicals { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}
