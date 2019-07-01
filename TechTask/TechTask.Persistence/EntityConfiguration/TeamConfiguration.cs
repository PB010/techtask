using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TechTask.Persistence.Models.Users;

namespace TechTask.Persistence.EntityConfiguration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.Property(b => b.HoursOfWorkOnAllTasks).IsRequired();

            builder.HasData(
                new 
                {
                    Id = 1,
                    Name = "Alpha",
                    HoursOfWorkOnAllTasks = 0,
                    CreatedAt = DateTime.Parse("01/07/2019 16:22"),
                    UpdatedAt = DateTime.Parse("01/07/2019 16:22")
                },
                new 
                {
                    Id = 2,
                    Name = "Beta",
                    HoursOfWorkOnAllTasks = 0,
                    CreatedAt = DateTime.Parse("01/07/2019 16:22"),
                    UpdatedAt = DateTime.Parse("01/07/2019 16:22")
                });
        }
    }
}
