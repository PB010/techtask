using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechTask.Persistence.Models.Task;

namespace TechTask.Persistence.EntityConfiguration
{
    public class TasksConfiguration : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Description).IsRequired().HasMaxLength(300);
            builder.Property(b => b.EstimatedTimeToFinish).IsRequired();
            builder.Property(b => b.Status).IsRequired();
        }
    }
}
