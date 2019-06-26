using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        }
    }
}
