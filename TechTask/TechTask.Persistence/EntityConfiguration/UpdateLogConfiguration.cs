using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TechTask.Persistence.Models.Logs;

namespace TechTask.Persistence.EntityConfiguration
{
    public class UpdateLogConfiguration : IEntityTypeConfiguration<UpdateLog>
    {
        public void Configure(EntityTypeBuilder<UpdateLog> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired();
            builder.Property(b => b.CreatedAt).IsRequired();

            builder.HasData(
                new UpdateLog
                {
                    Id = 1,
                    Name = "Added new User",
                    CreatedAt = DateTime.Parse("08 jul 19"),
                    UpdatedAt = null,
                    Value = "bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"
                },
                new UpdateLog
                {
                    Id = 2,
                    Name = "Added new User",
                    CreatedAt = DateTime.Parse("08 jul 19"),
                    UpdatedAt = null,
                    Value = "76aebd31-0235-4ef3-a123-08d6fbc1bdcd"
                },
                new UpdateLog
                {
                    Id = 3,
                    Name = "Added new User",
                    CreatedAt = DateTime.Parse("08 jul 19"),
                    UpdatedAt = null,
                    Value = "ed09fe47-84c0-47b5-8007-ae2ea4350a8b"
                },
                new UpdateLog
                {
                    Id = 4,
                    Name = "Added new User",
                    CreatedAt = DateTime.Parse("08 jul 19"),
                    UpdatedAt = null,
                    Value = "f3c88d42-fb42-43c4-a9d4-1a738a2bd20c"
                },
                new UpdateLog
                {
                    Id = 5,
                    Name = "Added new team",
                    CreatedAt = DateTime.Parse("08 jul 19"),
                    UpdatedAt = null,
                    Value = "1"
                },
                new UpdateLog
                {
                    Id = 6,
                    Name = "Added new team",
                    CreatedAt = DateTime.Parse("08 jul 19"),
                    UpdatedAt = null,
                    Value = "2"
                },
                new UpdateLog
                {
                    Id = 7,
                    Name = "Added new task",
                    CreatedAt = DateTime.Parse("08 jul 19"),
                    UpdatedAt = null,
                    Value = "1"
                },
                new UpdateLog
                {
                    Id = 8,
                    Name = "Added new log to task",
                    CreatedAt = DateTime.Parse("08 jul 19"),
                    UpdatedAt = null,
                    Value = "1"
                },
                new UpdateLog
                {
                    Id = 9,
                    Name = "Added new comment",
                    CreatedAt = DateTime.Parse("08 jul 19"),
                    UpdatedAt = null,
                    Value = "1"
                });
        }
    }
}
