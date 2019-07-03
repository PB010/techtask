using System.Collections.Generic;
using TechTask.Application.Users.Models;

namespace TechTask.Application.Teams.Models
{
    public class TeamDetailsDto
    {
        public int TeamId { get; set; }
        public string Name { get; set; }    
        public int HoursOfWorkOnAllTasks { get; set; }
        public IEnumerable<UserDetailsDto> Users { get; set; }
    }
}
