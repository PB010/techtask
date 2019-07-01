using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using TechTask.Application.TeamTasks.Models;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Task.Enums;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.TeamTasks.Commands
{
    public class CreateNewTaskCommand : IRequest<TaskDetailsDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTimeToFinishInHours { get; set; }
        public List<Comment> Comments => new List<Comment>();
        public List<LoggedActivity> Log => new List<LoggedActivity>();
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public WorkBalance Balance { get; set; }
        public int? TeamId { get; set; }
        public Guid? TrackerId { get; set; }
        public Guid? UserId { get; set; }
    }
}
