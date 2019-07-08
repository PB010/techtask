using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TechTask.Persistence.Models.Users;

namespace TechTask.Persistence.EntityConfiguration
{
    public class LoggedActivityConfiguration : IEntityTypeConfiguration<LoggedActivity>
    {
        public void Configure(EntityTypeBuilder<LoggedActivity> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Description).IsRequired().HasMaxLength(300);
            builder.Property(b => b.HoursSpent).IsRequired();
            builder.Property(b => b.TasksId).IsRequired();
            builder.Property(b => b.UserId).IsRequired();

            builder.HasData(
                new LoggedActivity
                {
                    Id = 1,
                    CreatedAt = DateTime.Parse("08 jul 19"),
                    Description = "Started working on it.",
                    HoursSpent = 1,
                    TasksId = 1,
                    UserId = Guid.Parse("76aebd31-0235-4ef3-a123-08d6fbc1bdcd")
                });
        }
    }
}
