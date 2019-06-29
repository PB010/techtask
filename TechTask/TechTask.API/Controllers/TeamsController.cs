using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechTask.Application.Teams.Commands;
using TechTask.Application.Teams.Models;

namespace TechTask.API.Controllers
{
    [Route("/api/teams/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TeamsController : BaseController
    {
        public TeamsController(IMediator mediator) : base(mediator)
        {}

        [HttpPost]
        public async Task<TeamDetailsDto> CreateTeam([FromBody] TeamForCreationCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
