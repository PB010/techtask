using AutoMapper;
using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Users.Models;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Users.Commands
{
    public class RegisterUserCommand : IRequest<UserForLoginDto>
    {
        public UserForRegistrationDto UserForRegistrationDto { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterUserCommand, UserForLoginDto>
    {
        private readonly IUserService _service;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IUserService userService, ITokenAuthenticationService authService,
            IMapper mapper)
        {
            _service = userService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<UserForLoginDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request.UserForRegistrationDto);
            user.Password = _authService.GenerateHashedPassword(request.UserForRegistrationDto.Password);

            await _service.AddUser(user);

            var userDto = _mapper.Map<UserForLoginDto>(user);
            userDto.Password = request.UserForRegistrationDto.Password;

            return userDto;
        }
    }

    public class UserForRegistrationValidation : AbstractValidator<UserForRegistrationDto>
    {
        public UserForRegistrationValidation(AppDbContext context)
        {
            RuleFor(x => x.Email).Must(m => !context.Users.Any(e => e.Email == m))
                .WithMessage("This email is already registered.")
                .NotEmpty().WithMessage("Email field shouldn't be empty or null.")
                .EmailAddress().WithMessage("Please provide a valid email format.");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password shouldn't be shorter than 6 characters.")
                .MaximumLength(100).WithMessage("Password shouldn't be longer than 100 characters.")
                .NotEmpty().WithMessage("Password field shouldn't be empty or null.");
            RuleFor(x => x.FirstName).MaximumLength(50).WithMessage("First Name shouldn't be longer than 50 characters.")
                .NotEmpty().WithMessage("First Name field shouldn't be empty or null.");
            RuleFor(x => x.LastName).MaximumLength(50).WithMessage("Last Name shouldn't be longer than 50 characters.")
                .NotEmpty().WithMessage("Last Name field shouldn't be empty or null.");
            RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("Date Of Birth field shouldn't be empty or null."); 
        }
    }
}
