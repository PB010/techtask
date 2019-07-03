using AutoMapper;
using TechTask.Application.Teams.Models;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Teams.Mapping
{
    public class TeamMappingProfile : Profile
    {
        public TeamMappingProfile()
        {
            CreateMap<Team, TeamDetailsDto>();
        }


    }
}
