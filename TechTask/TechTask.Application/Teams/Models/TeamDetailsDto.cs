using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Teams.Models
{
    public class TeamDetailsFullDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HoursOfWorkOnAllTasks { get; set; }
        public List<Tasks> Tasks { get; set; }
        public List<User> Users { get; set; }

        public static Expression<Func<Team, TeamDetailsFullDto>> ProjectionFull
        {
            get
            {
                return p => new TeamDetailsFullDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    HoursOfWorkOnAllTasks = p.HoursOfWorkOnAllTasks,
                    Tasks = p.Tasks,
                    Users = p.Users
                };
            }
        }

        public static TeamDetailsFullDto ConvertToTeamDtoFull(Team team)
        {
            return ProjectionFull.Compile().Invoke(team);
        }
    }
}
