using AutoMapper;
using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Users.Models;

namespace TechTask.Application.Users.Commands
{
    public class LoginUserCommand : IRequest<UserWithTokenDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserWithTokenDto>
    {
        private readonly ITokenAuthenticationService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public LoginUserCommandHandler(ITokenAuthenticationService authService,
            IUserService userService, IMapper mapper)
        {
            _authService = authService;
            _userService = userService;
            _mapper = mapper;
        }

        public Task<UserWithTokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var loggedInUser = _mapper.Map<UserWithTokenDto>(request.UserForLoginDto);
            loggedInUser.Token = _authService.GenerateToken(request.UserForLoginDto);

            return Task.FromResult(loggedInUser);
        }
    }

    public class UserForLoginValidator : AbstractValidator<UserForLoginDto>
    {
        public UserForLoginValidator(IUserService service)
        {
            RuleFor(x => new {x.Email, x.Password})
                .Must(m => service.UserExists(m.Email, m.Password)).WithErrorCode("404")
                .WithMessage("Invalid email or password, please try again.");
        }
    }
}
