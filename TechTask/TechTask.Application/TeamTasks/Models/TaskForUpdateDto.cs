using System;
using TechTask.Persistence.Models.Task.Enums;

namespace TechTask.Application.TeamTasks.Models
{
    public class TaskForUpdateDto
    {
        public int TaskId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int? EstimatedTimeToFinishInHours { get; set; }
        public int? TaskPriorityId { get; set; }
        public TaskStatus? Status { get; set; }
        public WorkBalance? Balance { get; set; }
        public TrackerTaskStatus? AdminApprovalOfTaskCompletion { get; set; }
        public Guid? TrackerId { get; set; }
        public string TrackerFirstName { get; set; }
        public string TrackerLastName { get; set; }
        public Guid? UserId { get; set; }
    }
}
