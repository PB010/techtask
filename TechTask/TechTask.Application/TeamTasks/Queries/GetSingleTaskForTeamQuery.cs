using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.TeamTasks.Models;

namespace TechTask.Application.TeamTasks.Queries
{
    public class GetSingleTaskForTeamQuery : IRequest<TaskDetailsDto>
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
    }

    public class GetSingleTaskForTeamHandler : IRequestHandler<GetSingleTaskForTeamQuery, TaskDetailsDto>
    {
        private readonly ITasksService _taskService;
        private readonly ITeamService _teamService;
        private readonly IHttpContextAccessor _accessor;

        public GetSingleTaskForTeamHandler(ITasksService taskService, ITeamService teamService,
            IHttpContextAccessor accessor)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public async Task<TaskDetailsDto> Handle(GetSingleTaskForTeamQuery request,
            CancellationToken cancellationToken)
        {
            var teamFromDb = await _teamService.GetTeamWithoutEagerLoadingAsync(request.TeamId);
            var taskFromDb = await _taskService.GetTaskAsync(request.Id, true);

            /// De scos in clasa aparte.
            //if (!_accessor.HttpContext.User.IsInRole("Admin") &&
            //    !_accessor.HttpContext.User.HasClaim(c => c.Type == "TeamId" &&
            //                                              c.Value == $"{teamFromDb.Id}"))
            //    throw new AuthenticationException("Unauthorized access.");  

            if (teamFromDb == null || taskFromDb == null)
                throw new ArgumentNullException();

            var taskToReturn = TaskDetailsDto.TaskDetailsFull(taskFromDb);

            return taskToReturn;
        }
    }


}
