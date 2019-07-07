using System;

namespace TechTask.Application.DbLogs.Models
{
    public class DbLogDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}
