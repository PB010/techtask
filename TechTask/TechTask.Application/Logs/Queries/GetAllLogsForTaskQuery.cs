using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Logs.Models;

namespace TechTask.Application.Logs.Queries
{
    public class GetAllLogsForTaskQuery : IRequest<IEnumerable<LogDetailsDto>>
    {
        public int TeamId { get; set; }
        public int TaskId { get; set; } 
    }

    public class GetAllLogsForTaskHandler : IRequestHandler<GetAllLogsForTaskQuery, IEnumerable<LogDetailsDto>>
    {
        private readonly ILogService _logService;
        private readonly ITasksService _tasksService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public GetAllLogsForTaskHandler(ILogService logService, ITasksService tasksService, 
            ITokenAuthenticationService authService, IMapper mapper)
        {
            _logService = logService;
            _tasksService = tasksService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LogDetailsDto>> Handle(GetAllLogsForTaskQuery request, CancellationToken cancellationToken)
        {
            var taskFromDb = await _tasksService.GetTaskWithoutEagerLoadingAsync(request.TaskId);

            if (!_authService.UserRoleAdminOrUserIdMatches(taskFromDb.UserId))
                throw new AuthenticationException();

            var logsFromDb = await _logService.GetAllLogsAsync(request.TaskId);

            return _mapper.Map<List<LogDetailsDto>>(logsFromDb);
        }
    }
}
