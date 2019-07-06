using FluentValidation;
using MediatR;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.TeamTasks.Models;

namespace TechTask.Application.TeamTasks.Commands
{
    public class RemoveUserFromTaskCommand : IRequest
    {
        public int TaskId { get; set; }
        public TaskForRemovalDto TaskForRemovalDto { get; set; }
        
    }

    public class RemoveUserFromTaskHandler : AsyncRequestHandler<RemoveUserFromTaskCommand>
    {
        private readonly ITasksService _taskService;
        private readonly ITokenAuthenticationService _authService;

        public RemoveUserFromTaskHandler(ITasksService taskService, ITokenAuthenticationService authService)
        {
            _taskService = taskService;
            _authService = authService;
        }

        protected override async Task Handle(RemoveUserFromTaskCommand request, CancellationToken cancellationToken)
        {
            var taskFromDb = await _taskService.GetTaskWithEagerLoadingAsync(request.TaskId);

            if (!_authService.UserRoleAdmin())
                throw new AuthenticationException("Only admins are allowed to remove users from task.");

            await _taskService.RemoveUserFromTaskAsync(taskFromDb, request.TaskForRemovalDto.TaskStatus);
        }
    }

    public class RemoveUserFromTaskValidator : AbstractValidator<TaskForRemovalDto>
    {
        public RemoveUserFromTaskValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Please provide a valid user id");
        }
    }
}
