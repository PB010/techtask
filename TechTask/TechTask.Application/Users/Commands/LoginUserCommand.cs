using FluentValidation;
using MediatR;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Users.Models;

namespace TechTask.Application.Users.Commands
{
    public class LoginUserCommand : IRequest<UserWithTokenDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public static Expression<Func<LoginUserCommand, UserWithTokenDto>> Projection
        {
            get
            {
                return p => new UserWithTokenDto
                {
                    Email = p.Email,
                    Status = "Successful login."
                };
            }
        }

        public static UserWithTokenDto ConvertToUserWithToken(LoginUserCommand command)
        {
            return Projection.Compile().Invoke(command);
        }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserWithTokenDto>
    {
        private readonly ITokenAuthenticationService _authService;
        private readonly IUserService _userService;

        public LoginUserCommandHandler(ITokenAuthenticationService authService,
            IUserService userService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public Task<UserWithTokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var loggedInUser = LoginUserCommand.ConvertToUserWithToken(request);
            loggedInUser.Token = _authService.GenerateToken(request);

            return Task.FromResult(loggedInUser);
        }
    }

    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator(IUserService service)
        {
            RuleFor(x => new {x.Email, x.Password})
                .Must(m => service.UserExists(m.Email, m.Password)).WithErrorCode("404")
                .WithMessage("Invalid email or password, please try again.");
        }
    }
}
