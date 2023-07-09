using Microsoft.EntityFrameworkCore;
using PROJ_Synchronise_Account_Api.Models.Data.Domain;

namespace PROJ_Synchronise_Account_Api.Models
{
    public class AppDbContext : DbContext
    {        
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }

        //public DbSet<CssProfile> CssProfiles { get; set; }
        //public DbSet<CssAccountProfile> CssAccountProfiles { get; set; }        
        public DbSet<CssAccount> CssAccounts { get; set; }
        public DbSet<Organisation> Organisations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("WaterCorp");
        }
    }
}
