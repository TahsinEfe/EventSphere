using Microsoft.EntityFrameworkCore;

namespace EventSphere
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entity mappings here
            // Example: modelBuilder.Entity<YourEntity>().ToTable("YourTableName");
        }
    }
   
}
