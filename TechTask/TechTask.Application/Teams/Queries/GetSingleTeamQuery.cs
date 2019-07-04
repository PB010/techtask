using AutoMapper;
using MediatR;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Teams.Models;

namespace TechTask.Application.Teams.Queries
{
    public class GetSingleTeamQuery : IRequest<TeamDetailsDto>
    {
        public int TeamId { get; set; }
    }

    public class GetSingleTeamHandler : IRequestHandler<GetSingleTeamQuery, TeamDetailsDto>
    {
        private readonly ITeamService _teamService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public GetSingleTeamHandler(ITeamService teamService, ITokenAuthenticationService authService,
            IMapper mapper)
        {   
            _teamService = teamService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<TeamDetailsDto> Handle(GetSingleTeamQuery request, CancellationToken cancellationToken)
        {
            var teamFromDb = await _teamService.GetTeamWithEagerLoadingAsync(request.TeamId);

            if (_authService.UserRoleAdminOrTeamIdMatches(teamFromDb))
                return _mapper.Map<TeamDetailsDto>(teamFromDb);

            throw new AuthenticationException();
        }
    }
}
