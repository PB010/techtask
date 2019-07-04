using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Persistence.Context;

namespace TechTask.Application.Teams.Commands
{
    public class RemoveUserFromTeamCommand : IRequest
    {
        public Guid UserId { get; set; }
    }

    public class RemoveUserFromTeamHandler : AsyncRequestHandler<RemoveUserFromTeamCommand>
    {
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;
        private readonly ITokenAuthenticationService _authService;

        public RemoveUserFromTeamHandler(IUserService userService, ITeamService teamService,
            ITokenAuthenticationService authService)
        {
            _userService = userService;
            _teamService = teamService;
            _authService = authService;
        }

        protected override async Task Handle(RemoveUserFromTeamCommand request, CancellationToken cancellationToken)
        {
            if (!_authService.UserRoleAdmin())
                throw new AuthenticationException("Unauthorized access.");

            var userForDb = await _userService.GetUserAsync(request.UserId);

            await _teamService.RemoveUserFromTeam(userForDb);
        }
    }

    public class RemoveUserFromTeamValidator : AbstractValidator<RemoveUserFromTeamCommand>
    {
        public RemoveUserFromTeamValidator(AppDbContext context)
        {
            RuleFor(x => x.UserId).Must(m => context.Users.Any(u => u.Id == m))
                .WithMessage("User doesn't exist.")
                .WithErrorCode("404");

            RuleFor(x => x.UserId).Must(m => context.Teams.Include(t => t.Users)
                    .Any(t => t.Users.Any(u => u.Id == m)))
                .WithMessage("This user is not a part of any team.")
                .WithErrorCode("400");
        }
    }
}
