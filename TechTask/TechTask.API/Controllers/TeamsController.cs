using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Application.Teams.Commands;
using TechTask.Application.Teams.Models;
using TechTask.Application.Teams.Queries;

namespace TechTask.API.Controllers
{
    [Route("/api/teams/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TeamsController : BaseController
    {
        public TeamsController(IMediator mediator) : base(mediator)
        { }

        [HttpPost]
        public async Task<TeamDetailsDto> CreateTeam([FromBody] TeamForCreationCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<IEnumerable<TeamDetailsDto>> GetAllTeams()
        {
            return await _mediator.Send(new GetAllTeamsQuery());
        }

        [HttpGet("{id}")]
        public async Task<TeamDetailsDto> GetSingleTeam([FromRoute] int id)
        {
            return await _mediator.Send(new GetSingleTeamQuery { Id = id });
        }

        [HttpPost("{id}")]
        public async Task<TeamDetailsDto> AssignUserToTeam([FromRoute] int id,
            [FromBody] IdAttributesDto dto)
        {
            dto.Id = id;

            return await _mediator.Send(new AssignUserToTeamCommand{IdAttributesDto = dto});
        }

        [HttpDelete("{id}/removeFromTeam")]
        public async Task<Unit> RemoveUserFromTeam([FromRoute] int id,
            [FromBody] RemoveUserFromTeamCommand command)
        {
            command.Id = id;

            return await _mediator.Send(command);
        }

        [HttpPatch("{id}")]
        public async Task<TeamDetailsDto> UpdateTeamName([FromRoute] int id,
            [FromBody] JsonPatchDocument<UpdateTeamNameCommand> name)
        {
            var command = new UpdateTeamNameCommand{Id = id};
            name.ApplyTo(command);

            return await _mediator.Send(command);
        }
    }
}
