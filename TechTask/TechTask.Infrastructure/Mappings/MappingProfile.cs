using AutoMapper;
using System;
using TechTask.Application.Users.Models;
using TechTask.Persistence.Models.Users;
using TechTask.Persistence.Models.Users.Enums;

namespace TechTask.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserForLoginDto>();

            CreateMap<UserForRegistrationDto, User>().ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => new Guid()))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(
                    src => Roles.User));

            CreateMap<UserForLoginDto, UserWithTokenDto>().ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => "Successful login."));


        }
    }
}
