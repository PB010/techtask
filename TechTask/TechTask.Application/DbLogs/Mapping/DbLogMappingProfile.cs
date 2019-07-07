using AutoMapper;
using TechTask.Application.DbLogs.Models;
using TechTask.Persistence.Models.Logs;

namespace TechTask.Application.DbLogs.Mapping
{
    public class DbLogMappingProfile : Profile
    {
        public DbLogMappingProfile()
        {
            CreateMap<UpdateLog, DbLogDetailsDto>().ForMember(dest => dest.CreatedAt, opt => 
                opt.MapFrom(src => src.CreatedAt.ToString("dd MMM yy")))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => 
                    src.UpdatedAt == null ? "" : src.UpdatedAt.Value.ToString("dd MMM yy")));
        }
    }
}
