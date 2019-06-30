using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
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

        public GetSingleTeamHandler(ITeamService teamService, IHttpContextAccessor accessor)
        {
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public async Task<TeamDetailsDto> Handle(GetSingleTeamQuery request, CancellationToken cancellationToken)
        {
            var teamFromDb = await _teamService.GetTeamAsync(request.Id, true);

            if (!_accessor.HttpContext.User.IsInRole("Admin") ||
                !_accessor.HttpContext.User.HasClaim(c => c.Type == ClaimTypes.Authentication &&
                                                         teamFromDb.Users.Any(t => $"{t.TeamId}" == c.Value)))
                throw new AuthenticationException();

            return TeamDetailsDto.ConvertToTeamDetailsDto(teamFromDb);
        }
    }
}
