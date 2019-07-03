using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Teams.Models;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Teams.Commands
{
    public class TeamForCreationCommand : IRequest<TeamDetailsDto>
    {
        public string Name { get; set; }

        public static Expression<Func<TeamForCreationCommand, Team>> Projection
        {
            get
            {
                return p => new Team
                {
                    Name = p.Name,
                    HoursOfWorkOnAllTasks = 0,
                };
            }
        }

        public static Team ConvertToTeam(TeamForCreationCommand command)
        {
            return Projection.Compile().Invoke(command);
        }
    }

    public class TeamForCreationHandler : IRequestHandler<TeamForCreationCommand, TeamDetailsDto>
    {
        private readonly ITeamService _teamService;
        private readonly IHttpContextAccessor _accessor;

        public TeamForCreationHandler(ITeamService teamService, IHttpContextAccessor accessor)
        {
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public async Task<TeamDetailsDto> Handle(TeamForCreationCommand request, CancellationToken cancellationToken)
        {
            if (!_accessor.HttpContext.User.IsInRole("Admin"))
                throw new AuthenticationException("You don't have permission to do that.");

            var teamToAdd = TeamForCreationCommand.ConvertToTeam(request);

            await _teamService.AddTeam(teamToAdd);
            //await _teamService.SaveChangesAsync();
            //
            //var teamToReturn = TeamDetailsDto.ConvertToTeamDetailsDto(teamToAdd);
            //teamToReturn.Tasks = new List<Tasks>();
            //teamToReturn.Users = new List<UserDetailsDto>();

            //return teamToReturn;

            throw new Exception();
        }
    }
}
