using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        }
    }
}
