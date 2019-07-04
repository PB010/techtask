using AutoMapper;
using MediatR;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.TeamTasks.Models;

namespace TechTask.Application.TeamTasks.Queries
{
    public class GetSingleTaskForTeamQuery : IRequest<TaskDetailsDto>
    {
        public int TaskId { get; set; } 
        public int TeamId { get; set; }
    }

    public class GetSingleTaskForTeamHandler : IRequestHandler<GetSingleTaskForTeamQuery, TaskDetailsDto>
    {
        private readonly ITasksService _taskService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public GetSingleTaskForTeamHandler(ITasksService taskService, ITokenAuthenticationService authService,
            IMapper mapper)
        {
            _taskService = taskService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<TaskDetailsDto> Handle(GetSingleTaskForTeamQuery request,
            CancellationToken cancellationToken)
        {

            if (!_authService.UserRoleAdminOrTeamIdMatches(request.TeamId))
                    throw new AuthenticationException("Unauthorized access.");  

            var taskFromDb = await _taskService.GetTaskWithEagerLoadingAsync(request.TaskId);

            return _mapper.Map<TaskDetailsDto>(taskFromDb);
        }
    }


}
