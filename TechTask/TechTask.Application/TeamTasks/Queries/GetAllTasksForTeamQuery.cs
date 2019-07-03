using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.TeamTasks.Models;

namespace TechTask.Application.TeamTasks.Queries
{
    public class GetAllTasksForTeamQuery : IRequest<IEnumerable<TaskDetailsDto>>
    {
        public int TeamId { get; set; }
    }
        
    public class GetAllTasksForTeamHandler : IRequestHandler<GetAllTasksForTeamQuery, IEnumerable<TaskDetailsDto>>
    {
        private readonly ITasksService _taskService;
        private readonly ITeamService _teamService;

        public GetAllTasksForTeamHandler(ITasksService taskService, ITeamService teamService)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _teamService = teamService ?? throw new ArgumentNullException(nameof(teamService));
        }

        public async Task<IEnumerable<TaskDetailsDto>> Handle(GetAllTasksForTeamQuery request, CancellationToken cancellationToken)
        {
            var teamForTask = await _teamService.GetTeamWithoutEagerLoadingAsync(request.TeamId);

            if (teamForTask == null)
                throw new ArgumentNullException();

            var tasksFromDb = await _taskService.GetAllTasksAsync();
            var tasksToReturn = tasksFromDb.Select(TaskDetailsDto.TaskDetailsFull);

            return tasksToReturn;
        }
    }
}
