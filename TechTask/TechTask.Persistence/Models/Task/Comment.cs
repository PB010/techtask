using System;

namespace TechTask.Persistence.Models.Task
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int TasksId { get; set; }
        public Guid UserId { get; set; }
    }
}
    