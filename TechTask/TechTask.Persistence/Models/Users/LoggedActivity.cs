using System;
using TechTask.Persistence.Common;

namespace TechTask.Persistence.Models.Users
{
    public class LoggedActivity : IBaseClass
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int HoursSpent { get; set; }
        public int TasksId { get; set; }    
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}
