using System;

namespace TechTask.Persistence.Models.Users
{
    public class LoggedActivity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime HoursSpent { get; set; }
        public int TasksId { get; set; }    
        public Guid UserId { get; set; }
    }
}
