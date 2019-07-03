using AutoMapper;
using TechTask.Application.TeamTasks.Models;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Task.Enums;

namespace TechTask.Application.TeamTasks.Mapping
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<Tasks, TaskDetailsDto>().ForMember(dest => dest.TaskId, opt =>
                    opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Balance, opt =>
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

            CreateMap<TaskForCreationDto, Tasks>().ForMember(dest => dest.Balance, opt =>
                    opt.MapFrom(src => WorkBalance.Excellent))
                .ForMember(dest => dest.AdminApprovalOfTaskCompletion, opt =>
                    opt.MapFrom(src => TrackerTaskStatus.NotEvaluatedYet))
                .ForMember(dest => dest.TotalHoursOfWork, opt =>
                    opt.MapFrom(src => 0))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    src.UserId == null ? TaskStatus.Unassigned : TaskStatus.Assigned));
        }
    }
}
