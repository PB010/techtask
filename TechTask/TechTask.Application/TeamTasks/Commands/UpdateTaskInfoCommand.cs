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

        public UpdateTaskInfoHandler(ITasksService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        public async Task<TaskDetailsDto> Handle(UpdateTaskInfoCommand request, CancellationToken cancellationToken)
        {
            var taskFromDb = await _taskService.GetTaskWithEagerLoadingAsync(request.TaskForUpdateDto.TaskId);

            await _taskService.UpdateTask(taskFromDb, request.TaskForUpdateDto);

            return _mapper.Map<TaskDetailsDto>(taskFromDb);
        }
    }
}
