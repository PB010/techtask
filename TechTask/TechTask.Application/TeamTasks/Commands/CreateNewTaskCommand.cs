﻿using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.TeamTasks.Models;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Task;

namespace TechTask.Application.TeamTasks.Commands
{
    public class CreateNewTaskCommand : IRequest<TaskDetailsDto>
    {
        public TaskForCreationDto TaskForCreationDto { get; set; }
    }

    public class CreateNewTaskHandler : IRequestHandler<CreateNewTaskCommand, TaskDetailsDto>
    {
        private readonly ITasksService _tasksService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;

        public CreateNewTaskHandler(ITasksService tasksService, IUserService userService,   
            IHttpContextAccessor accessor, IMapper mapper)
        {
            _tasksService = tasksService;
            _userService = userService;
            _accessor = accessor;
            _mapper = mapper;
        }   

        public async Task<TaskDetailsDto> Handle(CreateNewTaskCommand request, CancellationToken cancellationToken)
        {
            var taskToAdd = _mapper.Map<Tasks>(request.TaskForCreationDto);

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

            await _tasksService.AddTask(taskToAdd);

            var taskFromDbForMapping = await _tasksService.GetTaskWithEagerLoadingAsync(taskToAdd.Id);
            var taskToReturn = _mapper.Map<TaskDetailsDto>(taskFromDbForMapping);
          
            return taskToReturn;
        }   
    }

    public class CreateNewTaskValidator : AbstractValidator<TaskForCreationDto>
    {
        public CreateNewTaskValidator(AppDbContext context)
        {
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Task name is too long.")
                .NotNull().NotEmpty().WithMessage("Please provide a valid name.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description is too long.")
                .NotNull().NotEmpty().WithMessage("Please provide a valid description.");
            RuleFor(x => x.EstimatedTimeToFinishInHours).NotEmpty()
                .WithMessage("Please provide a valid time to finish.");
            RuleFor(x => x.PriorityId).Must(m => context.TaskPriorities
                    .Any(p => p.Id == m))
                .NotEmpty().WithMessage("Please provide a valid priority.");

            When(c => c.UserId != null, () =>
            {
                RuleFor(x => new {x.UserId, x.TeamId}).Must(m => context.Teams
                        .Include(t => t.Users)
                        .Single(t => t.Id == m.TeamId).Users.Any(u => u.Id == m.UserId))
                    .WithMessage("This user is not a part of the team.");
            });
        }
    }
}