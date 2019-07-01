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
        public string Priority { get; set; }
        public TaskStatus Status { get; set; }
        public WorkBalance Balance { get; set; }
        public string TeamName { get; set; }
        public string TrackerName { get; set; }
        public string UserOnTask { get; set; } 
    }
}       
