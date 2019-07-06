using System;
using System.Collections.Generic;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users.Enums;

namespace TechTask.Persistence.Models.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public DateTime DateOfBirth { get; set; }
        public Roles Role { get; set; }
        public List<Tasks> Tasks { get; set; }
        public List<Comment> Comments { get; set; }
        public List<LoggedActivity> Log { get; set; } 
        public int? TeamId { get; set; }
        public Team Team { get; set; }  
    }
}   
