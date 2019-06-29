using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Teams.Models;

namespace TechTask.Application.Teams.Commands
{
    public class TeamForCreationCommand : IRequest<TeamDetailsDto>
    {
        public string Name { get; set; }
    }

    public class TeamForCreationHandler : IRequestHandler<TeamForCreationCommand, TeamDetailsDto>
    {
        public async Task<TeamDetailsDto> Handle(TeamForCreationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
