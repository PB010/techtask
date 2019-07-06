using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.TeamTasks.Models;

namespace TechTask.Application.TeamTasks.Commands
{
    public class UpdateTaskInfoCommand : IRequest<TaskDetailsDto>
    {
        public TaskForUpdateDto TaskForUpdateDto { get; set; }
    }

    public class UpdateTaskInfoHandler : IRequestHandler<UpdateTaskInfoCommand, TaskDetailsDto>
    {
        private readonly ITasksService _taskService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public UpdateTaskInfoHandler(ITasksService taskService, IMapper mapper,
            IEmailService emailService)
        {
            _taskService = taskService;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<TaskDetailsDto> Handle(UpdateTaskInfoCommand request, CancellationToken cancellationToken)
        {
            var taskFromDb = await _taskService.GetTaskWithEagerLoadingAsync(request.TaskForUpdateDto.TaskId);

            await _emailService.SendEmailIfStatusChangedAsync(taskFromDb, request.TaskForUpdateDto.Status);
            await _taskService.UpdateTask(taskFromDb, request.TaskForUpdateDto);

            return _mapper.Map<TaskDetailsDto>(taskFromDb);
        }
    }
}
