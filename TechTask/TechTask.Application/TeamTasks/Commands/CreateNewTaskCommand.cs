using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.TeamTasks.Models;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Task.Enums;
using TechTask.Persistence.Models.Users;
using TaskStatus = TechTask.Persistence.Models.Task.Enums.TaskStatus;

namespace TechTask.Application.TeamTasks.Commands
{
    public class CreateNewTaskCommand : IRequest<TaskDetailsDto>
    {
        public int TeamId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int EstimatedTimeToFinishInHours { get; set; }
        public List<Comment> Comments => new List<Comment>();
        public List<LoggedActivity> Log => new List<LoggedActivity>();
        public int PriorityId { get; set; } 
        public Guid? TrackerId { get; set; }
        public Guid? UserId { get; set; }

        public static Expression<Func<CreateNewTaskCommand, Tasks>> Projection
        {
            get
            {
                return p => new Tasks
                {
                    Name = p.Name,
                    Description = p.Description,
                    Balance = WorkBalance.Excellent,
                    AdminApprovalOfTaskCompletion = TrackerTaskStatus.NotEvaluatedYet,
                    Comments = p.Comments,
                    Log = p.Log,
                    EstimatedTimeToFinishInHours = p.EstimatedTimeToFinishInHours,
                    TaskPriorityId = p.PriorityId,
                    TotalHoursOfWork = 0,
                    TrackerId = p.TrackerId,
                    UserId = p.UserId,
                    TeamId = p.TeamId
                };
            }
        }
            
        public static Tasks ConvertToTask(CreateNewTaskCommand command)
        {
            return Projection.Compile().Invoke(command);
        }
    }

    public class CreateNewTaskHandler : IRequestHandler<CreateNewTaskCommand, TaskDetailsDto>
    {
        private readonly ITasksService _tasksService;
        private readonly ITeamService _teamService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _accessor;

        public CreateNewTaskHandler(ITasksService tasksService, ITeamService teamService,
            IUserService userService, IHttpContextAccessor accessor)
        {
            _tasksService = tasksService;
            _teamService = teamService;
            _userService = userService;
            _accessor = accessor;
        }

        public async Task<TaskDetailsDto> Handle(CreateNewTaskCommand request, CancellationToken cancellationToken)
        {
            throw new EncoderFallbackException();
            var teamFromDb = await _teamService.GetTeamWithEagerLoadingAsync(request.TeamId);
            if (teamFromDb == null)
                throw new ArgumentNullException();
            
            if (request.UserId != null &&
                teamFromDb.Users.All(u => u.Id != request.UserId))
            { 
                throw new HttpRequestException("Bad request");
            }
            
            var taskToAdd = CreateNewTaskCommand.ConvertToTask(request);
            taskToAdd.Status = taskToAdd.UserId == null ? TaskStatus.Unassigned : TaskStatus.Assigned;
            taskToAdd.TrackerId = _accessor.HttpContext.User.IsInRole("Admin")
                ? new Guid(_accessor.HttpContext.User.Claims.Single(c => c.Type == "UserId").Value)
                : taskToAdd.TrackerId;

            if (taskToAdd.TrackerId != null)
            {
                var userToMap = await _userService
                    .GetUserAsync(taskToAdd.TrackerId ??
                                  throw new ArgumentNullException());

                taskToAdd.TrackerFirstName = userToMap.FirstName;
                taskToAdd.TrackerLastName = userToMap.LastName;
            }

            _tasksService.AddTask(taskToAdd);
            //await _tasksService.SaveChangesAsync();

            var taskFromDbForMapping = await _tasksService.GetTaskAsync(taskToAdd.Id, true);
            //var taskToReturn = TaskDetailsDto.TaskDetailsWithNoUsers(taskFromDbForMapping);
            //taskToReturn.TrackerName = taskFromDbForMapping.TrackerId == null
            //    ? null
            //    : $"{taskToAdd.TrackerFirstName} {taskToAdd.TrackerLastName}";
            //
            //if (taskFromDbForMapping.User == null)
            //    return taskToReturn;
            //
            //taskToReturn.UserOnTask = $"{taskFromDbForMapping.User.FirstName} {taskFromDbForMapping.User.LastName}";
            //
            //return taskToReturn;
        }   
    }

    public class CreateNewTaskValidator : AbstractValidator<CreateNewTaskCommand>
    {
        public CreateNewTaskValidator(AppDbContext context)
        {
            RuleFor(x => x.Name).MaximumLength(100).NotNull().NotEmpty();
            RuleFor(x => x.Description).MaximumLength(500).NotNull().NotEmpty();
            RuleFor(x => x.EstimatedTimeToFinishInHours).NotEmpty();
            RuleFor(x => x.PriorityId).Must(m => context.TaskPriorities
                    .Any(p => p.Id == m))
                .NotEmpty();
        }
    }
}