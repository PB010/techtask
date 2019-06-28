using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;

namespace TechTask.Application.Users.Commands
{
    public class RemoveUserFromTeamCommand : IRequest
    {
        public Guid Id { get; set; }    
    }

    public class RemoveUserFromTeamHandler : AsyncRequestHandler<RemoveUserFromTeamCommand>
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _accessor;

        public RemoveUserFromTeamHandler(IUserService userService, IHttpContextAccessor accessor)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        protected override Task Handle(RemoveUserFromTeamCommand request, CancellationToken cancellationToken)
        {
            if (!_accessor.HttpContext.User.IsInRole("Admin"))
                throw new AuthenticationException("Unauthorized access.");

            _userService.RemoveUserFromTeamAsync(request.Id);

            return Task.CompletedTask;
        }
    }

    public class RemoveUserFromTeamValidator : AbstractValidator<RemoveUserFromTeamCommand>
    {
        public RemoveUserFromTeamValidator(IUserService service)
        {
            RuleFor(x => x.Id).Must(service.UserExists)
                .WithMessage("Wrong user.")
                .WithErrorCode("404");
        }
    }

}
