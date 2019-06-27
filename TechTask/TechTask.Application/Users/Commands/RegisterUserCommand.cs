using FluentValidation;
using MediatR;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Users.Models;
using TechTask.Persistence.Models.Users;
using TechTask.Persistence.Models.Users.Enums;

namespace TechTask.Application.Users.Commands
{
    public class RegisterUserCommand : IRequest<UserForLoginDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Roles Role { get; set; }
        public int? TeamId { get; set; }

        public static Expression<Func<RegisterUserCommand, User>> Projection
        {
            get
            {
                return p => new User
                {
                    Id = new Guid(),
                    Email = p.Email,
                    Password = p.Password,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    DateOfBirth = p.DateOfBirth,
                    Role = p.Role
                };
            }
        }

        public static User ConvertRegisteredUser(RegisterUserCommand command)
        {
            return Projection.Compile().Invoke(command);
        }
            
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterUserCommand, UserForLoginDto>
    {
        private readonly IUserService _service;

        public RegisterCommandHandler(IUserService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<UserForLoginDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = RegisterUserCommand.ConvertRegisteredUser(request);

            _service.AddUser(user);
            await _service.SaveChangesAsync();

            var loginUser = UserForLoginDto.ConvertToLoginDto(user);

            return loginUser;
        }
    }

    public class RegisterCommandValidation : AbstractValidator<RegisterUserCommand>
    {
        public RegisterCommandValidation()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.FirstName).MaximumLength(50).NotEmpty();
            RuleFor(x => x.LastName).MaximumLength(50).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.Role).NotEmpty();
        }
    }
}
