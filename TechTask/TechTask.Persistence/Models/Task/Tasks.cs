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
        public DateTime EstimatedTimeToFinish { get; set; }
        public DateTime TotalHoursOfWork { get; set; }   
        public List<Comment> Comments { get; set; }
        public List<LoggedActivity> Log { get; set; }   
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public WorkBalance Balance { get; set; }
        public int? TeamId { get; set; }     
        public Guid? TrackerId { get; set; }
        public Guid? AssigneeId { get; set; }    
    }
}
        