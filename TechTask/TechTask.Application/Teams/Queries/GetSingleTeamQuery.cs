using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Teams.Models;

namespace TechTask.Application.Teams.Queries
{
    public class GetSingleTeamQuery : IRequest<TeamDetailsDto>
    {
        public int Id { get; set; }
    }

    public class GetSingleTeamHandler : IRequestHandler<GetSingleTeamQuery, TeamDetailsDto>
    {
        private readonly ITeamService _teamService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;

        public GetSingleTeamHandler(ITeamService teamService, IHttpContextAccessor accessor,
            IMapper mapper)
        {
            _teamService = teamService;
            _accessor = accessor;
            _mapper = mapper;
        }

        public async Task<TeamDetailsDto> Handle(GetSingleTeamQuery request, CancellationToken cancellationToken)
        {
            var teamFromDb = await _teamService.GetTeamWithEagerLoadingAsync(request.Id);

            if (_accessor.HttpContext.User.IsInRole("Admin") ||
                _accessor.HttpContext.User.HasClaim(c => c.Type == "TeamId" &&
                                                         teamFromDb.Users.Any(t => $"{t.TeamId}" == c.Value)))
                return _mapper.Map<TeamDetailsDto>(teamFromDb);

            throw new AuthenticationException();
        }
    }
}
