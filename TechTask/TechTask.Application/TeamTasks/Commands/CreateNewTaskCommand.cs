using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.TeamTasks.Models;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task;
using TechTask.Persistence.Models.Task.Enums;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.TeamTasks.Commands
{
    public class CreateNewTaskCommand : IRequest<TaskDetailsDto>
    {
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
                    Comments = p.Comments,
                    Log = p.Log,
                    EstimatedTimeToFinishInHours = p.EstimatedTimeToFinishInHours,
                    //Priority = p.PriorityId
                };
            }
        }
    }

    public class CreateNewTaskHandler : IRequestHandler<CreateNewTaskCommand, TaskDetailsDto>
    {
        private readonly ITasksService _tasksService;
        private readonly IHttpContextAccessor _accessor;

        public CreateNewTaskHandler(ITasksService tasksService, IHttpContextAccessor accessor)
        {
            _tasksService = tasksService ?? throw new ArgumentNullException(nameof(tasksService));
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public async Task<TaskDetailsDto> Handle(CreateNewTaskCommand request, CancellationToken cancellationToken)
        {
            throw new AbandonedMutexException();
        }
    }

    public class CreateNewTaskValidator : AbstractValidator<CreateNewTaskCommand>
    {
        public CreateNewTaskValidator(AppDbContext context)
        {
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(500).NotEmpty();
            RuleFor(x => x.EstimatedTimeToFinishInHours).NotEmpty();
            RuleFor(x => x.PriorityId).Must(m => context.TaskPriorities.Any(p => p.Id == m)).NotEmpty();
        }
    }
}
