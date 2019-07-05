using AutoMapper;
using MediatR;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Logs.Models;

namespace TechTask.Application.Logs.Queries
{
    public class GetLogForTaskCommand : IRequest<LogDetailsDto>
    {
        public int TaskId { get; set; }
        public int TeamId { get; set; }
        public int LogId { get; set; }  
    }
    
    public class GetLogForTaskHandler : IRequestHandler<GetLogForTaskCommand, LogDetailsDto>
    {
        private readonly ILogService _logService;
        private readonly ITasksService _taskService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public GetLogForTaskHandler(ILogService logService, ITasksService taskService,
            ITokenAuthenticationService authService, IMapper mapper)
        {
            _logService = logService;
            _taskService = taskService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<LogDetailsDto> Handle(GetLogForTaskCommand request, CancellationToken cancellationToken)
        {
            var taskFromDb = await _taskService.GetTaskWithoutEagerLoadingAsync(request.TaskId);

            if (!_authService.UserRoleAdminOrUserIdMatches(taskFromDb.UserId))
                throw new AuthenticationException();

            var logFromDb = await _logService.GetLogAsync(request.LogId);
            var logToReturn = _mapper.Map<LogDetailsDto>(logFromDb);
            _logService.AssignDateTimeToLogDetailsDto(logToReturn);

            return logToReturn;
        }
    }
}
