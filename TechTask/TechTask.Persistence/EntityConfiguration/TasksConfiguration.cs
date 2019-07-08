using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Task.Enums;

namespace TechTask.Persistence.EntityConfiguration
{
    public class TasksConfiguration : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(50);
            builder.Property(b => b.Description).IsRequired().HasMaxLength(300);
            builder.Property(b => b.EstimatedTimeToFinishInHours).IsRequired();
            builder.Property(b => b.Status).IsRequired();

            builder.HasData(
                new Tasks
                {
                    Id = 1,
                    Name = "Maintenance",
                    Description = "Weekly maintenance",
                    Status = TaskStatus.Assigned,
                    AdminApprovalOfTaskCompletion = TrackerTaskStatus.NotEvaluatedYet,
                    Balance = WorkBalance.Excellent,
                    EstimatedTimeToFinishInHours = 4,
                    TaskPriorityId = 2,
                    TeamId = 1,
                    TotalHoursOfWork = 1,
                    TrackerFirstName = "Will",
                    TrackerLastName = "Stevens",
                    TrackerId = Guid.Parse("bfd23b66-1a4e-41de-4aa7-08d6fae3b08b"),
                    UserId = Guid.Parse("76aebd31-0235-4ef3-a123-08d6fbc1bdcd")
                });
        }
    }
}
