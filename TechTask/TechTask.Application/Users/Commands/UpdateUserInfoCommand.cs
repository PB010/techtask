using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Users.Models;

namespace TechTask.Application.Users.Commands
{
    public class UpdateUserInfoCommand : IRequest<UserDetailsDto>
    {
        public UserForUpdateDto UserForUpdateDto { get; set; }
    }

    public class UpdateUserInfoHandler : IRequestHandler<UpdateUserInfoCommand, UserDetailsDto>
    {
        private readonly IUserService _userService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public UpdateUserInfoHandler(IUserService userService, ITokenAuthenticationService authService,
            IMapper mapper)
        {
            _userService = userService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<UserDetailsDto> Handle(UpdateUserInfoCommand request, CancellationToken cancellationToken)
        {
            if (!_authService.UserRoleAdmin())
                throw new AuthenticationException();

            var userFromDb = await _userService.GetUserAsync(request.UserForUpdateDto.UserId
            ?? throw new ArgumentNullException());
            await _userService.UpdateUser(userFromDb, request.UserForUpdateDto);
            userFromDb = await _userService.GetUserAsync(userFromDb.Id);

            return _mapper.Map<UserDetailsDto>(userFromDb);
        }
    }

    public class UpdateUserInfoValidator : AbstractValidator<UserForUpdateDto>
    {
        public UpdateUserInfoValidator()
        {
            
            RuleFor(x => new {x.Role, x.TeamId}).NotEmpty().NotNull()
                .WithMessage("You have to provide both a new Team Id and User Role to update this user.");
            
        }
    }
}
