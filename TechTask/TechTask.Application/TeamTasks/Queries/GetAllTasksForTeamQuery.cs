using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
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
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public GetAllTasksForTeamHandler(ITasksService taskService, ITokenAuthenticationService authService,
            IMapper mapper)
        {
            _taskService = taskService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDetailsDto>> Handle(GetAllTasksForTeamQuery request,
            CancellationToken cancellationToken)
        {
            if (!_authService.UserRoleAdminOrTeamIdMatches(request.TeamId))
                throw new AuthenticationException("Access denied");

            var tasksFromDb = await _taskService.GetAllTasksForATeamAsync(request.TeamId);
            var tasksToReturn = tasksFromDb.Select(t => _mapper.Map<TaskDetailsDto>(t));

            return tasksToReturn;
        }
    }
}
