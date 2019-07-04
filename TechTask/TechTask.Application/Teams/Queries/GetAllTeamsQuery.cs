using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Teams.Models;

namespace TechTask.Application.Teams.Queries
{
    public class GetAllTeamsQuery : IRequest<IEnumerable<TeamDetailsDto>>
    {
    }

    public class GetAllTeamsHandler : IRequestHandler<GetAllTeamsQuery, IEnumerable<TeamDetailsDto>>
    {
        private readonly ITeamService _teamService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public GetAllTeamsHandler(ITeamService teamService, ITokenAuthenticationService authService,
            IMapper mapper)
        {
            _teamService = teamService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamDetailsDto>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
        {
            if (!_authService.UserRoleAdmin())
            {
                throw new AuthenticationException("Access denied.");
            }

            var teamsForAdmins = await _teamService.GetAllTeamsWithEagerLoadingAsync();

            return _mapper.Map<IEnumerable<TeamDetailsDto>>(teamsForAdmins);
        }
    }
}
