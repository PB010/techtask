using System;
using System.Collections.Generic;
using TechTask.Persistence.Common;
using TechTask.Persistence.Models.Task.Enums;
using TechTask.Persistence.Models.Users;

namespace TechTask.Persistence.Models.Task
{
    public class Tasks : IBaseClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTimeToFinishInHours { get; set; }
        public int TotalHoursOfWork { get; set; }   
        public List<Comment> Comments { get; set; }
        public List<LoggedActivity> Log { get; set; }   
        public int TaskPriorityId { get; set; }
        public TaskPriority TaskPriority { get; set; }  
        public TaskStatus Status { get; set; }
        public WorkBalance Balance { get; set; }  
        public TrackerTaskStatus AdminApprovalOfTaskCompletion { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public Guid? TrackerId { get; set; }
        public string TrackerFirstName { get; set; }
        public string TrackerLastName { get; set; } 
        public Guid? UserId { get; set; }
        public User User { get; set; }  
    }
}
        