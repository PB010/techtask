using System;
using TaskStatus = TechTask.Persistence.Models.Task.Enums.TaskStatus;

namespace TechTask.Application.Logs.Models
{
    public class LogForCreationDto
    {
        public string Description { get; set; }
        public int HoursSpent { get; set; }
        public int TasksId { get; set; }
        public Guid UserId { get; set; }
        public int TeamId { get; set; }
        public TaskStatus TaskStatus { get; set; }

    }
}
