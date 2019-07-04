using AutoMapper;
using TechTask.Application.Comments.Models;
using TechTask.Persistence.Models.Task;

namespace TechTask.Application.Comments.Mapping
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Comment, CommentDetailsDto>().ForMember(dest => dest.CommentId, opt =>
                opt.MapFrom(src => src.Id));
            CreateMap<CommentForCreationDto, Comment>();
        }
    }
}
