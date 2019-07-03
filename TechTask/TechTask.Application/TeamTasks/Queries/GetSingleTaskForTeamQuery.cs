using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.TeamTasks.Models;

namespace TechTask.Application.TeamTasks.Queries
{
    public class GetSingleTaskForTeamQuery : IRequest<TaskDetailsDto>
    {
        public int TaskId { get; set; } 
        public int TeamId { get; set; }
    }

    public class GetSingleTaskForTeamHandler : IRequestHandler<GetSingleTaskForTeamQuery, TaskDetailsDto>
    {
        private readonly ITasksService _taskService;
        private readonly ITeamService _teamService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;

        public GetSingleTaskForTeamHandler(ITasksService taskService, ITeamService teamService,
            IHttpContextAccessor accessor, IMapper mapper)
        {
            _taskService = taskService;
            _teamService = teamService;
            _accessor = accessor;
            _mapper = mapper;
        }

        public async Task<TaskDetailsDto> Handle(GetSingleTaskForTeamQuery request,
            CancellationToken cancellationToken)
        {
            var teamFromDb = await _teamService.GetTeamWithoutEagerLoadingAsync(request.TeamId);
            var taskFromDb = await _taskService.GetTaskAsync(request.TaskId, true);

            /// De scos in clasa aparte.
            //if (!_accessor.HttpContext.User.IsInRole("Admin") &&
            //    !_accessor.HttpContext.User.HasClaim(c => c.Type == "TeamId" &&
            //                                              c.Value == $"{teamFromDb.UserId}"))
            //    throw new AuthenticationException("Unauthorized access.");  

            //if (teamFromDb == null || taskFromDb == null)
            //    throw new ArgumentNullException();

            return _mapper.Map<TaskDetailsDto>(taskFromDb);
        }
    }


}
