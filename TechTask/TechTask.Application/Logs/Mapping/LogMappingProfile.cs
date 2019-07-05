using AutoMapper;
using TechTask.Application.Logs.Models;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Logs.Mapping
{
    public class LogMappingProfile : Profile
    {
        public LogMappingProfile()
        {
            CreateMap<LogForCreationDto, LoggedActivity>();
            CreateMap<LoggedActivity, LogDetailsDto>().ForMember(dest => dest.LogId, opt =>
                opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
        }

    }
}
