using System.Collections.Generic;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.TeamTasks.Models
{
    public class TaskDetailsDto
    {
        public int TaskId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTimeToFinishInHours { get; set; }
        public int TotalHoursOfWork { get; set; }
        public List<Comment> Comments { get; set; }
        public List<LoggedActivity> Log { get; set; }
        public TaskPriority Priority { get; set; }
        public string Status { get; set; }
        public string Balance { get; set; }
        public string AdminApprovalOfTaskCompletion { get; set; }
        public string TeamName { get; set; }
        public string TrackerName { get; set; }
        public string UserOnTask { get; set; }
    }
}
