using AutoMapper;
using MediatR;
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
    }
        
    public class GetAllTasksForTeamHandler : IRequestHandler<GetAllTasksForTeamQuery, IEnumerable<TaskDetailsDto>>
    {
        private readonly ITasksService _taskService;
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public GetAllTasksForTeamHandler(ITasksService taskService, ITeamService teamService,
            IMapper mapper)
        {
            _taskService = taskService;
            _teamService = teamService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDetailsDto>> Handle(GetAllTasksForTeamQuery request,
            CancellationToken cancellationToken)
        {
            var tasksFromDb = await _taskService.GetAllTasksAsync();
            var tasksToReturn = tasksFromDb.Select(t => _mapper.Map<TaskDetailsDto>(t));

            return tasksToReturn;
        }
    }
}
