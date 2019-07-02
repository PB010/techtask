using AutoMapper;
using System;
using TechTask.Application.Users.Models;
using TechTask.Persistence.Models.Users;
using TechTask.Persistence.Models.Users.Enums;

namespace TechTask.Infrastructure.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserForLoginDto>();

            CreateMap<UserForRegistrationDto, User>().ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => new Guid()))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(
                    src => Roles.User));

            CreateMap<UserForLoginDto, UserWithTokenDto>().ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => "Successful login."));

            CreateMap<User, UserDetailsDto>().ForMember(dest => dest.Age, opt =>
                opt.MapFrom(src => (DateTime.Now.Year - src.DateOfBirth.Year).ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                    $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.NumberOfComments, opt => opt.MapFrom(src =>
                    src.Comments.Count))
                .ForMember(dest => dest.NumbersOfLogs, opt => opt.MapFrom(src =>
                    src.Log.Count))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src =>
                    src.Role.ToString()));
        }
    }
}
