using System;
using TechTask.Persistence.Models.Users.Enums;

namespace TechTask.Application.Users.Models
{
    public class UserForUpdateDto
    {
        public Roles? Role { get; set; }
        public int? TeamId { get; set; }
        public Guid? UserId { get; set; } 
    }
}
