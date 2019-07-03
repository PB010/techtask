using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Authentication;
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
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;

        public GetSingleTaskForTeamHandler(ITasksService taskService, IHttpContextAccessor accessor,
            IMapper mapper)
        {
            _taskService = taskService;
            _accessor = accessor;
            _mapper = mapper;
        }

        public async Task<TaskDetailsDto> Handle(GetSingleTaskForTeamQuery request,
            CancellationToken cancellationToken)
        {

            if (!_accessor.HttpContext.User.IsInRole("Admin") &&
                    _accessor.HttpContext.User.HasClaim(c => c.Type == "TeamId" &&
                                                              c.Value != $"{request.TeamId}"))
                    throw new AuthenticationException("Unauthorized access.");  

            var taskFromDb = await _taskService.GetTaskWithEagerLoadingAsync(request.TaskId);

            /// De scos in clasa aparte.
            //if (!_accessor.HttpContext.User.IsInRole("Admin") &&
            //    !_accessor.HttpContext.User.HasClaim(c => c.Type == "TeamId" &&
            //                                              c.Value == $"{teamFromDb.UserId}"))
            //    throw new AuthenticationException("Unauthorized access.");  

            return _mapper.Map<TaskDetailsDto>(taskFromDb);
        }
    }


}
