using System;
using TechTask.Persistence.Models.Task.Enums;

namespace TechTask.Application.TeamTasks.Models
{
    public class TaskForRemovalDto
    {
        public Guid UserId { get; set; }
        public TaskStatus TaskStatus { get; set; }
    }
}
