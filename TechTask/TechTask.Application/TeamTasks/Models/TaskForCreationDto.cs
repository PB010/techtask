using System;
using System.Collections.Generic;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.TeamTasks.Models
{
    public class TaskForCreationDto
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTimeToFinishInHours { get; set; }
        public List<Comment> Comments => new List<Comment>();
        public List<LoggedActivity> Log => new List<LoggedActivity>();
        public int PriorityId { get; set; }
        public Guid? TrackerId { get; set; }
        public Guid? UserId { get; set; } 
    }
}
