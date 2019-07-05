using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Logs.Models;
using TechTask.Application.TeamTasks.Models;
using TechTask.Persistence.Models.Users;

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
        private readonly ITeamService _teamService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public AddLogToATaskHandler(ILogService logService, ITasksService taskService,
            ITeamService teamService, ITokenAuthenticationService authService, IMapper mapper)  
        {
            _logService = logService;
            _taskService = taskService;
            _teamService = teamService; 
            _authService = authService;
            _mapper = mapper;
        }
            
        public async Task<TaskDetailsDto> Handle(AddLogToATaskCommand request, CancellationToken cancellationToken)
        {
            var taskFromDb = await _taskService.GetTaskWithEagerLoadingAsync(request.LogForCreationDto.TasksId);

            if (!_authService.UserRoleAdminOrUserIdMatches(taskFromDb.UserId))
                throw new AuthenticationException();

            request.LogForCreationDto.UserId = new Guid(_authService.GetUserIdClaimValue());

            var logToAdd = _mapper.Map<LoggedActivity>(request.LogForCreationDto);
            await _logService.AddNewLog(logToAdd);
            await _taskService.CalculateNewWorkBalanceAsync(taskFromDb, logToAdd);
            await _taskService.ChangeStatusBasedOnAdminApproval(taskFromDb, request.LogForCreationDto);

            var teamForUpdate = await _teamService.GetTeamWithoutEagerLoadingAsync(request.LogForCreationDto.TeamId);
            await _teamService.CalculateTotalHoursOfWorkAsync(teamForUpdate, taskFromDb);

            var taskToReturn  = _mapper.Map<TaskDetailsDto>(taskFromDb);
            _taskService.AssignDateTimeToCreatedAt(taskToReturn);

            return taskToReturn;
        }
    }
    
    public class AddLogToTaskValidator : AbstractValidator<LogForCreationDto>
    {
        public AddLogToTaskValidator()
        {
            RuleFor(x => x.HoursSpent).NotEmpty();
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please specify the work you've done on this request.");
            RuleFor(x => x.TaskStatus).NotEmpty();
        }
    }
}
