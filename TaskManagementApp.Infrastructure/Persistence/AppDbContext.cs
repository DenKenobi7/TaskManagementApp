using Microsoft.EntityFrameworkCore;
using TaskManagementApp.Domain;
using TaskManagementApp.Infrastructure.Persistence.Configurations;

namespace TaskManagementApp.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TaskEntity> Tasks { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskEntityConfiguration());
        }
    }
}
