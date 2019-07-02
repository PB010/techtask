using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TechTask.Application.Users.Models;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Teams.Models
{
    public class TeamDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HoursOfWorkOnAllTasks { get; set; }
        public List<Tasks> Tasks { get; set; }
        public IEnumerable<UserDetailsDto> Users { get; set; }

        public static Expression<Func<Team, TeamDetailsDto>> Projection
        {
            get
            {
                return p => new TeamDetailsDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    HoursOfWorkOnAllTasks = p.HoursOfWorkOnAllTasks,
                    Tasks = p.Tasks,
                    //Users = p.Users.Select(UserDetailsDto.ConvertToUserDetails)
                };
            }
        }

        public static TeamDetailsDto ConvertToTeamDetailsDto(Team team)
        {
            return Projection.Compile().Invoke(team);
        }
    }
}
