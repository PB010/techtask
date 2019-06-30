using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public GetAllTeamsHandler(ITeamService teamService, IHttpContextAccessor accessor)
        {
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public async Task<IEnumerable<TeamDetailsDto>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
        {
            if (!_accessor.HttpContext.User.IsInRole("Admin"))
            {
                var teamsFromDb = await _teamService.GetAllTeamsAsync(false);

                return teamsFromDb.Select(TeamDetailsDto.ConvertToTeamDetailsDto);
            }

            var teamsForAdmins = await _teamService.GetAllTeamsAsync(true);

            return teamsForAdmins.Select(TeamDetailsDto.ConvertToTeamDetailsDto);
        }
    }
}
