using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Teams.Models;

namespace TechTask.Application.Teams.Commands
{
    public class UpdateTeamNameCommand : IRequest<TeamDetailsDto>
    {
        public int Id { get; set; } 
        public string Name { get; set; }
    }

    public class UpdateTeamNameHandler : IRequestHandler<UpdateTeamNameCommand, TeamDetailsDto>
    {
        private readonly ITeamService _teamService;
        private readonly IHttpContextAccessor _accessor;

        public UpdateTeamNameHandler(ITeamService teamService, IHttpContextAccessor accessor)
        {
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public async Task<TeamDetailsDto> Handle(UpdateTeamNameCommand request, CancellationToken cancellationToken)
        {
            if (!_accessor.HttpContext.User.IsInRole("Admin"))
                throw new AuthenticationException("You don't have permission to do that.");

            var teamFromDb = await _teamService.GetTeamAsync(request.Id, true);

            if (teamFromDb == null)
                throw new ArgumentNullException();

            teamFromDb.Name = request.Name;
            //await _teamService.SaveChangesAsync();
            //
            //return TeamDetailsDto.ConvertToTeamDetailsDto(teamFromDb);

            throw new Exception();
        }
    }
}   
