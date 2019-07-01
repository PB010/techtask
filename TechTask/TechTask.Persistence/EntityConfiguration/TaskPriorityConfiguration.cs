﻿using Microsoft.EntityFrameworkCore;
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
                new 
                {
                    Id = 1,
                    Name = "Low",
                    CreatedAt = DateTime.Parse("01/07/2019 16:22"),
                    UpdatedAt = DateTime.Parse("01/07/2019 16:22")
                },
                new 
                {
                    Id = 2,
                    Name = "Normal",
                    CreatedAt = DateTime.Parse("01/07/2019 16:22"),
                    UpdatedAt = DateTime.Parse("01/07/2019 16:22")
                },
                new 
                {
                    Id = 3,
                    Name = "Urgent",
                    CreatedAt = DateTime.Parse("01/07/2019 16:22"),
                    UpdatedAt = DateTime.Parse("01/07/2019 16:22")
                });
        }
    }
}
