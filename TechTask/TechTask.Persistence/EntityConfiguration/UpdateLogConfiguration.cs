using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        }
    }
}
