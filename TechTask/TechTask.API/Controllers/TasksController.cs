using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Application.Filters.Validators.GeneralValidator;
using TechTask.Application.Filters.Validators.TaskValidator;
using TechTask.Application.TeamTasks.Commands;
using TechTask.Application.TeamTasks.Models;
using TechTask.Application.TeamTasks.Queries;

namespace TechTask.API.Controllers
{
    [Route("/api/teams/{teamId}/tasks/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [ServiceFilter(typeof(ValidateRouteAttributes))]
    public class TasksController : BaseController
    {
        public TasksController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateTaskForCreationDto))]
        public async Task<TaskDetailsDto> CreateATask([FromRoute] int teamId,
            [FromBody] TaskForCreationDto dto)
        {
            dto.TeamId = teamId;

            return await _mediator.Send(new CreateNewTaskCommand {TaskForCreationDto = dto});
        }

        [HttpGet]
        public async Task<IEnumerable<TaskDetailsDto>> GetAllTasksForTeam([FromRoute] int teamId)
        {
            return await _mediator.Send(new GetAllTasksForTeamQuery {TeamId = teamId});
        }

        [HttpGet("{taskId}")]
        public async Task<TaskDetailsDto> GetTaskForTeam([FromRoute] int teamId,
            [FromRoute] int taskId)
        {
            return await _mediator.Send(new GetSingleTaskForTeamQuery {TaskId = taskId, TeamId = teamId});
        }

        [HttpPost("{taskId}")]
        [ServiceFilter(typeof(ValidateAssignToUserCommand))]
        public async Task<TaskDetailsDto> AssignUserToTask([FromRoute] int teamId,
            [FromRoute] int taskId, [FromBody] AssignUserToTaskCommand command)
        {
            command.TaskId = taskId;

            return await _mediator.Send(command);
        }

        [HttpDelete("{taskId}")]
        [ServiceFilter(typeof(ValidateRemoveUserFromTaskCommand))]
        public async Task<Unit> RemoveUserFromTask([FromRoute] int teamId,
            [FromRoute] int taskId, [FromBody] TaskForRemovalDto dto)
        {
            

            return await _mediator.Send(new RemoveUserFromTaskCommand
            {
                TaskId = taskId,
                TaskForRemovalDto = dto
            });
        }

        [HttpPatch("{taskId}")]
        [ServiceFilter(typeof(ValidateTaskForUpdate))]
        public async Task<TaskDetailsDto> UpdateTaskInfo([FromRoute] int teamId,
            [FromRoute] int taskId, [FromBody] JsonPatchDocument<TaskForUpdateDto> dto)
        {
            var command = new UpdateTaskInfoCommand
            {
                TaskForUpdateDto = new TaskForUpdateDto()
            };

            dto.ApplyTo(command.TaskForUpdateDto);
            command.TaskForUpdateDto.TaskId = taskId;

            return await _mediator.Send(command);
        }

        [HttpPost("{taskId}/approveCompletionRequest")]
        [ServiceFilter(typeof(ValidateApproveTaskCompletion))]
        public async Task<TaskDetailsDto> ApproveTaskCompletion([FromRoute] int teamId,
            [FromRoute] int taskId)
        {
            return await _mediator.Send(new ApproveTaskCompletionCommand {TaskId = taskId});
        }

        [HttpDelete("{taskId}/approveCompletionRequest")]
        [ServiceFilter(typeof(ValidateApproveTaskCompletion))]
        public async Task<TaskDetailsDto> DenyTaskCompletion([FromRoute] int teamId,
            [FromRoute] int taskId)
        {
            return await _mediator.Send(new DenyTaskCompletionCommand {TaskId = taskId});
        }

        [HttpPut("{taskId}/reopenTask")]
        [ServiceFilter(typeof(ValidateReopenTask))]
        public async Task<TaskDetailsDto> ReopenTask([FromRoute] int teamId,
            [FromRoute] int taskId)
        {
            return await _mediator.Send(new ReopenTaskCommand {TaskId = taskId});
        }

    }
}
