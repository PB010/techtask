﻿using AutoMapper;
using TechTask.Application.TeamTasks.Models;
using TechTask.Persistence.Models.Task;

namespace TechTask.Application.TeamTasks.Mapping
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<Tasks, TaskDetailsDto>().ForMember(dest => dest.Balance, opt =>
                    opt.MapFrom(src => src.Balance.ToString()))
                .ForMember(dest => dest.TeamName, opt => opt.MapFrom(src =>
                    src.Team.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    src.Status.ToString()))
                .ForMember(dest => dest.AdminApprovalOfTaskCompletion, opt => opt.MapFrom(
                    src => src.AdminApprovalOfTaskCompletion.ToString()))
                .ForMember(dest => dest.UserOnTask, opt => opt.MapFrom(src =>
                    $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.TrackerName, opt => opt.MapFrom(src =>
                    $"{src.TrackerFirstName} {src.TrackerLastName}"));
        }
    }
}