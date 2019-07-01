using System;
using System.Collections.Generic;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Task.Enums;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.TeamTasks.Models
{
    public class TaskDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTimeToFinishInHours { get; set; }
        public int TotalHoursOfWork { get; set; }
        public List<Comment> Comments { get; set; }
        public List<LoggedActivity> Log { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public WorkBalance Balance { get; set; }
        public int? TeamId { get; set; }
        public Guid? TrackerId { get; set; }
        public Guid? UserId { get; set; }
    }
}
