using System;
using System.Collections.Generic;
using TechTask.Application.TeamTasks.Models;

namespace TechTask.Application.Users.Models
{
    public class UserDetailsDto
    {
        public Guid UserId { get; set; }    
        public string Email { get; set; }
        public string Name { get; set; }            
        public string Age { get; set; }
        public string Role { get; set; }
        public string TeamName { get; set; } 
        public IEnumerable<TaskDetailsDto> Tasks { get; set; }
        public int NumberOfComments { get; set; }
        public int NumbersOfLogs { get; set; }
    }
}
