using System;

namespace TechTask.Application.Logs.Models
{
    public class LogDetailsDto
    {
        public int LogId { get; set; }
        public string Description { get; set; }
        public int HoursSpent { get; set; } 
        public int TasksId { get; set; }
        public Guid UserId { get; set; }
        public string CreatedAt { get; set; }
    }
}
