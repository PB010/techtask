using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.TeamTasks.Models;

namespace TechTask.Application.TeamTasks.Queries
{
    public class GetAllTasksForTeamQuery : IRequest<IEnumerable<TaskDetailsDto>>
    {
        public int TeamId { get; set; } 
    }
        
    public class GetAllTasksForTeamHandler : IRequestHandler<GetAllTasksForTeamQuery, IEnumerable<TaskDetailsDto>>
    {
        private readonly ITasksService _taskService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IMapper _mapper;

        public GetAllTasksForTeamHandler(ITasksService taskService, IHttpContextAccessor accessor,
            IMapper mapper)
        {
            _taskService = taskService;
            _accessor = accessor;   
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDetailsDto>> Handle(GetAllTasksForTeamQuery request,
            CancellationToken cancellationToken)
        {
            if (!_accessor.HttpContext.User.IsInRole("Admin") &&
                _accessor.HttpContext.User.HasClaim(c => c.Type == "TeamId" &&
                                                         c.Value != $"{request.TeamId}"))
                throw new AuthenticationException("Access denied");

            var tasksFromDb = await _taskService.GetAllTasksForATeamAsync(request.TeamId);
            var tasksToReturn = tasksFromDb.Select(t => _mapper.Map<TaskDetailsDto>(t));

            return tasksToReturn;
        }
    }
}
