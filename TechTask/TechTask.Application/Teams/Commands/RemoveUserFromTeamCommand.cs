using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public int Id { get; set; }
        public Guid UserId { get; set; }
    }

    public class RemoveUserFromTeamHandler : AsyncRequestHandler<RemoveUserFromTeamCommand>
    {
        private readonly IUserService _userService;
        private readonly ITeamService _teamService;
        private readonly IHttpContextAccessor _accessor;

        public RemoveUserFromTeamHandler(IUserService userService, ITeamService teamService,
            IHttpContextAccessor accessor)
        {
            _userService = userService;
            _teamService = teamService;
            _accessor = accessor;
        }

        protected override async Task Handle(RemoveUserFromTeamCommand request, CancellationToken cancellationToken)
        {
            if (!_accessor.HttpContext.User.IsInRole("Admin"))
                throw new AuthenticationException("Unauthorized access.");

            var userForDb = await _userService.GetUserAsync(request.UserId);
            var teamForDb = await _teamService.GetTeamWithoutEagerLoadingAsync(request.Id);

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

    //public class RemoveUserFromTeamRouteValidator : AbstractValidator<int>
    //{
    //    public RemoveUserFromTeamRouteValidator(AppDbContext context)
    //    {
    //        RuleFor(x => x).Must(m => context.Teams.Any(t => t.Id == m))
    //            .WithMessage("This team was not found.")
    //            .WithErrorCode("404");
    //    }
    //}

}
