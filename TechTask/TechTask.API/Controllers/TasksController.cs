using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Application.TeamTasks.Commands;
using TechTask.Application.TeamTasks.Models;
using TechTask.Application.TeamTasks.Queries;

namespace TechTask.API.Controllers
{
    [Route("/api/teams/{teamId}/tasks/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TasksController : BaseController
    {
        public TasksController(IMediator mediator) : base(mediator)
        {}

        [HttpPost]
        public async Task<TaskDetailsDto> CreateATask([FromRoute] int teamId,
            [FromBody] CreateNewTaskCommand command)
        {
            command.TeamId = teamId;

            return await _mediator.Send(command);
        }

        [HttpGet]
        public async Task<IEnumerable<TaskDetailsDto>> GetAllTasksForTeam([FromRoute] int teamId)
        {
            return await _mediator.Send(new GetAllTasksForTeamQuery());
        }

        [HttpGet("{taskId}")]
        public async Task<TaskDetailsDto> GetTaskForTeam([FromRoute] int teamId,
            [FromRoute] int taskId)
        {
            return await _mediator.Send(new GetSingleTaskForTeamQuery {TaskId = taskId, TeamId = teamId});
        }
    }
}
