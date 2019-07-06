using AutoMapper;
using MediatR;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.TeamTasks.Models;

namespace TechTask.Application.TeamTasks.Commands
{
    public class ReopenTaskCommand : IRequest<TaskDetailsDto>
    {
        public int TaskId { get; set; } 
    }

    public class ReopenTaskHandler : IRequestHandler<ReopenTaskCommand, TaskDetailsDto>
    {
        private readonly ITasksService _taskService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public ReopenTaskHandler(ITasksService taskService, ITokenAuthenticationService authService,
            IMapper mapper, IEmailService emailService)
        {
            _taskService = taskService;
            _authService = authService;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<TaskDetailsDto> Handle(ReopenTaskCommand request, CancellationToken cancellationToken)
        {
            if(!_authService.UserRoleAdmin())
                throw new AuthenticationException();

            var taskFromDb = await _taskService.GetTaskWithEagerLoadingAsync(request.TaskId);
            await _taskService.ReopenTask(taskFromDb);
            await _emailService.SendEmailAsync(
                "admin@tech.com",
                "Status change",
                $"Status for task '{taskFromDb.Name}' has changed to {taskFromDb.Status.ToString()}");

            return _mapper.Map<TaskDetailsDto>(taskFromDb);
        }
    }
}
