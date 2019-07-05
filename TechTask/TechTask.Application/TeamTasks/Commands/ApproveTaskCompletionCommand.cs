using AutoMapper;
using MediatR;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.TeamTasks.Models;

namespace TechTask.Application.TeamTasks.Commands
{
    public class ApproveTaskCompletionCommand : IRequest<TaskDetailsDto>
    {
        public int TaskId { get; set; } 
    }

    public class ApproveTaskCompletionHandler : IRequestHandler<ApproveTaskCompletionCommand, TaskDetailsDto>
    {
        private readonly ITasksService _taskService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public ApproveTaskCompletionHandler(ITasksService taskService, ITokenAuthenticationService authService,
            IMapper mapper)
        {
            _taskService = taskService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<TaskDetailsDto> Handle(ApproveTaskCompletionCommand request,
            CancellationToken cancellationToken)
        {
            if (!_authService.UserRoleAdmin())
                throw new AuthenticationException();

            var taskFromDb = await _taskService.GetTaskWithEagerLoadingAsync(request.TaskId);
            await _taskService.ChangeTasksAdminApprovalState(taskFromDb);

            return _mapper.Map<TaskDetailsDto>(taskFromDb);
        }
    }
}
