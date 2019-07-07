using System;

namespace TechTask.Persistence.Models.Logs
{
    public class UpdateLog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    
}
    