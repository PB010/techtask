using AutoMapper;
using MediatR;
using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using TechTask.Application.Interfaces;
using TechTask.Application.TeamTasks.Models;

namespace TechTask.Application.TeamTasks.Commands
{
    public class AssignUserToTaskCommand : IRequest<TaskDetailsDto>
    {
        public int TaskId { get; set; }
        public Guid UserId { get; set; }    
    }

    public class AssignUserToTaskHandler : IRequestHandler<AssignUserToTaskCommand, TaskDetailsDto>
    {
        private readonly ITasksService _taskService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public AssignUserToTaskHandler(ITasksService taskService, ITokenAuthenticationService authService,
            IMapper mapper) 
        {
            _taskService = taskService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<TaskDetailsDto> Handle(AssignUserToTaskCommand request, CancellationToken cancellationToken)
        {
            var taskFromDb = await _taskService.GetTaskWithEagerLoadingAsync(request.TaskId);

            if (!_authService.UserRoleAdminOrUserIdMatches(request.UserId))
                throw new AuthenticationException("You don't have permission to assign anyone else beside yourself to a task.");

            await _taskService.AddUserToTaskAsync(taskFromDb, request.UserId);

            return _mapper.Map<TaskDetailsDto>(taskFromDb);
        }
    }

    public class AssignUserToTaskValidator : AbstractValidator<AssignUserToTaskCommand>
    {
        public AssignUserToTaskValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("Please provide a valid user id");
        }
    }
}
