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
            var check = CreateMap<LoggedActivity, LogDetailsDto>().ForMember(dest => dest.LogId, opt =>
                opt.MapFrom(src => src.Id));
        }

    }
}
