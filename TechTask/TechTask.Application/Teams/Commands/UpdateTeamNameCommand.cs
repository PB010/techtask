using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Teams.Models;

namespace TechTask.Application.Teams.Commands
{
    public class UpdateTeamNameCommand : IRequest<TeamDetailsDto>
    {
        public int TeamId { get; set; } 
        public string Name { get; set; }
    }   

    public class UpdateTeamNameHandler : IRequestHandler<UpdateTeamNameCommand, TeamDetailsDto>
    {
        private readonly ITeamService _teamService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;

        public UpdateTeamNameHandler(ITeamService teamService, IHttpContextAccessor accessor,
            IMapper mapper)
        {
            _teamService = teamService;
            _accessor = accessor;
            _mapper = mapper;
        }

        public async Task<TeamDetailsDto> Handle(UpdateTeamNameCommand request, CancellationToken cancellationToken)
        {
            if (!_accessor.HttpContext.User.IsInRole("Admin"))
                throw new AuthenticationException("You don't have permission to do that.");

            var teamFromDb = await _teamService.GetTeamWithEagerLoadingAsync(request.TeamId);
            _teamService.UpdateTeamName(teamFromDb, request.Name);

            return _mapper.Map<TeamDetailsDto>(teamFromDb);
        }
    }
}   
