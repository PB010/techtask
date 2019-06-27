using System;
using System.Collections.Generic;
using System.Text;
using TechTask.Persistence.Models.Users.Enums;

namespace TechTask.Application.Users
{
    public class UserForCreationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Roles Role { get; set; }
        public int TeamId { get; set; }
    }
}
