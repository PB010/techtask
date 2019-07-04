using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;
using TechTask.Application.Logs.Commands;
using TechTask.Application.Logs.Models;
using TechTask.Application.TeamTasks.Models;

namespace TechTask.API.Controllers
{
    [Route("/api/teams/{teamId}/tasks/{taskId}/logs/")]
    public class LogsController : BaseController
    {
        public LogsController(IMediator mediator) : base(mediator)
        {}

        [HttpPost]
        public async Task<TaskDetailsDto> AddNewLog([FromRoute] int teamId,
            [FromRoute] int taskId, [FromBody] LogForCreationDto dto)
        {
            dto.TasksId = taskId;

            return await _mediator.Send(new AddLogToATaskCommand {LogForCreationDto = dto});
        }
    }
}
