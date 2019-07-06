using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TechTask.Persistence.Models.Task;

namespace TechTask.Persistence.EntityConfiguration
{
    public class TaskPriorityConfiguration : IEntityTypeConfiguration<TaskPriority>
    {
        public void Configure(EntityTypeBuilder<TaskPriority> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);

            builder.HasData(
                new TaskPriority
                {
                    Id = 1,
                    Name = "Low"
                },
                new TaskPriority
                {
                    Id = 2,
                    Name = "Normal"
                },
                new TaskPriority
                {
                    Id = 3,
                    Name = "Urgent"
                });
        }
    }
}
