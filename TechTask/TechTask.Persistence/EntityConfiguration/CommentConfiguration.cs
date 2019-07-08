using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TechTask.Persistence.Models.Task;

namespace TechTask.Persistence.EntityConfiguration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Description).IsRequired().HasMaxLength(300);
            builder.Property(b => b.UserId).IsRequired();
            builder.Property(b => b.TasksId).IsRequired();

            builder.HasData(
                new Comment
                {
                    Id = 1,
                    TasksId = 1,
                    Description = "I'm pleased with your work for now.",
                    UserId = Guid.Parse("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b")
                });
        }
    }
}
