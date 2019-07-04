using AutoMapper;
using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Teams.Models;
using TechTask.Application.Users.Models;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Teams.Commands
{
    public class TeamForCreationCommand : IRequest<TeamDetailsDto>
    {
        public string Name { get; set; }
    }

    public class TeamForCreationHandler : IRequestHandler<TeamForCreationCommand, TeamDetailsDto>
    {
        private readonly ITeamService _teamService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public TeamForCreationHandler(ITeamService teamService, ITokenAuthenticationService authService,
            IMapper mapper)
        {
            _teamService = teamService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<TeamDetailsDto> Handle(TeamForCreationCommand request, CancellationToken cancellationToken)
        {
            if (!_authService.UserRoleAdmin())
                throw new AuthenticationException("You don't have permission to do that.");

            var teamToAdd = _mapper.Map<Team>(request);

            await _teamService.AddTeam(teamToAdd);

            var teamToReturn = _mapper.Map<TeamDetailsDto>(teamToAdd);
            teamToReturn.Users = new List<UserDetailsDto>();

            return teamToReturn;
        }
    }

    public class TeamForCreationValidator : AbstractValidator<TeamForCreationCommand>
    {
        public TeamForCreationValidator()
        {
            RuleFor(x => x.Name).MaximumLength(40).WithMessage("Team name is too long.")
                .NotEmpty().WithMessage("Please provide a team name");
        }
    }
}
