using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;

        public GetAllTeamsHandler(ITeamService teamService, IHttpContextAccessor accessor,
            IMapper mapper)
        {
            _teamService = teamService;
            _accessor = accessor;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamDetailsDto>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
        {
            if (!_accessor.HttpContext.User.IsInRole("Admin"))
            {
                throw new AuthenticationException("Access denied.");
            }

            var teamsForAdmins = await _teamService.GetAllTeamsWithEagerLoadingAsync();

            return _mapper.Map<IEnumerable<TeamDetailsDto>>(teamsForAdmins);
        }
    }
}
