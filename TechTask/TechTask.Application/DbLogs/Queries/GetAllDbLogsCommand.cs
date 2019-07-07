using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.DbLogs.Models;
using TechTask.Application.Interfaces;

namespace TechTask.Application.DbLogs.Queries
{
    public class GetAllDbLogsCommand : IRequest<IEnumerable<DbLogDetailsDto>>
    {
        public DbLogQueryParameters DbLogQueryParameters { get; set; }
    }

    public class GetAllDbLogsHandler : IRequestHandler<GetAllDbLogsCommand, IEnumerable<DbLogDetailsDto>>
    {
        private readonly IDbLogService _dbLogService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public GetAllDbLogsHandler(IDbLogService dbLogService, ITokenAuthenticationService authService,
            IMapper mapper)
        {
            _dbLogService = dbLogService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DbLogDetailsDto>> Handle(GetAllDbLogsCommand request,
            CancellationToken cancellationToken)
        {
            if (!_authService.UserRoleAdmin())
                throw new AuthenticationException();

            var logs = await _dbLogService.GetAllLogs(request.DbLogQueryParameters);

            return _mapper.Map<IEnumerable<DbLogDetailsDto>>(logs);
        }
    }
}
