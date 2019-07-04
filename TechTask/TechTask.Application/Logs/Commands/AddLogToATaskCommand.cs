using MediatR;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Logs.Models;
using TechTask.Application.TeamTasks.Models;

namespace TechTask.Application.Logs.Commands
{
    public class AddLogToATaskCommand : IRequest<TaskDetailsDto>
    {
        public LogForCreationDto LogForCreationDto { get; set; }
    }

    public class AddLogToATaskHandler : IRequestHandler<AddLogToATaskCommand, TaskDetailsDto>
    {
        private readonly ILogService _logService;
        private readonly ITasksService _taskService;
        private readonly ITokenAuthenticationService _authService;

        public AddLogToATaskHandler(ILogService logService, ITasksService taskService,
            ITokenAuthenticationService authService)
        {
            _logService = logService;
            _taskService = taskService;
            _authService = authService;
        }

        public async Task<TaskDetailsDto> Handle(AddLogToATaskCommand request, CancellationToken cancellationToken)
        {
            var taskFromDb = await _taskService.GetTaskWithoutEagerLoadingAsync(request.LogForCreationDto.TasksId);

            if (!_authService.UserRoleAdminOrWorkingOnTask(taskFromDb))
                throw new AuthenticationException();


        }
    }
}
