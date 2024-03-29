﻿using AutoMapper;
using TechTask.Application.Teams.Commands;
using TechTask.Application.Teams.Models;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Teams.Mapping
{
    public class TeamMappingProfile : Profile
    {
        public TeamMappingProfile()
        {
            CreateMap<Team, TeamDetailsDto>().ForMember(dest => dest.TeamId, opt => 
                opt.MapFrom(src => src.Id));

            CreateMap<TeamForCreationCommand, Team>().ForMember(dest => dest.HoursOfWorkOnAllTasks,
                opt => opt.MapFrom(src => 0));
        }
    }
}
