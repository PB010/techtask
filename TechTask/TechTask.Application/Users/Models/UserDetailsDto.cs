using System;
using System.Collections.Generic;
using TechTask.Persistence.Models.Task;

namespace TechTask.Application.Users.Models
{
    public class UserDetailsDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }        
        public string Age { get; set; }
        public string Role { get; set; }
        public List<Tasks> Tasks { get; set; }
        public int NumberOfComments { get; set; }
        public int NumbersOfLogs { get; set; }
    }
}
