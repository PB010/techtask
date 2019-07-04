using System.Collections.Generic;
using TechTask.Application.Comments.Models;
using TechTask.Application.Logs.Models;
using TechTask.Persistence.Models.Task;

namespace TechTask.Application.TeamTasks.Models
{
    public class TaskDetailsDto
    {
        public int TaskId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTimeToFinishInHours { get; set; }
        public int TotalHoursOfWork { get; set; }
        public List<CommentDetailsDto> Comments { get; set; }
        public List<LogDetailsDto> Log { get; set; }
        public TaskPriority TaskPriority { get; set; }  
        public string Status { get; set; }
        public string Balance { get; set; }
        public string AdminApprovalOfTaskCompletion { get; set; }
        public string TeamName { get; set; }
        public string TrackerName { get; set; }
        public string UserOnTask { get; set; }
    }
}
