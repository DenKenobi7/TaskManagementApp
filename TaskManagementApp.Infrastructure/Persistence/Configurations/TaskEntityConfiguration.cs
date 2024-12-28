using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementApp.Domain;

namespace TaskManagementApp.Infrastructure.Persistence.Configurations;
public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.HasKey(t => t.ID);
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(t => t.Description)
            .HasMaxLength(100);
        builder.Property(t => t.Status)
            .IsRequired();
        builder.Property(t => t.AssignedTo)
            .HasMaxLength(100);
    }
}
