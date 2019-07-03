using System.Collections.Generic;
using TechTask.Application.TeamTasks.Models;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Teams.Models
{
    public class TeamDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HoursOfWorkOnAllTasks { get; set; }
        public IEnumerable<TaskDetailsDto> Tasks { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
