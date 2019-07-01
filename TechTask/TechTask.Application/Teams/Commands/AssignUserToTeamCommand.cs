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
using TechTask.Application.Teams.Models;
using TechTask.Persistence.Context;

namespace TechTask.Application.Teams.Commands
{
    public class AssignUserToTeamCommand : IRequest<TeamDetailsDto>
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
    }

    public class AssignUserToTeamHandler : IRequestHandler<AssignUserToTeamCommand, TeamDetailsDto>
    {
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _accessor;

        public AssignUserToTeamHandler(ITeamService teamService, IUserService userService,
            IHttpContextAccessor accessor)
        {
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public async Task<TeamDetailsDto> Handle(AssignUserToTeamCommand request, CancellationToken cancellationToken)
        {
            var teamFromDb = await _teamService.GetTeamAsync(request.Id, true);
            var userFromDb = await _userService.GetUserAsync(request.UserId);

            if (teamFromDb == null)
                throw new ArgumentNullException();

            if (!_accessor.HttpContext.User.IsInRole("Admin"))
                throw new AuthenticationException("You don't have permission to do that.");

            teamFromDb.Users.Add(userFromDb);
            await _teamService.SaveChangesAsync();

            var teamToReturn = TeamDetailsDto.ConvertToTeamDetailsDto(teamFromDb);

            return teamToReturn;
        }

        public class AssignUserToTeamValidator : AbstractValidator<AssignUserToTeamCommand>
        {
            public AssignUserToTeamValidator(AppDbContext context)
            {
                RuleFor(x => x.UserId).Must(m => context.Users.Any(u => u.Id == m))
                    .WithMessage("There is no such user in DB.")
                    .WithErrorCode("404");
                
                RuleFor(x => x.UserId)
                    .Must(m => context.Teams.Include(t => t.Users)
                        .All(t => t.Users.Any(u => u.Id != m)))
                    .WithMessage("This user is already a part of a team.")
                    .WithErrorCode("400");
            }
        }
    }
}
