using AutoMapper;
using MediatR;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.DbLogs.Models;
using TechTask.Application.Interfaces;

namespace TechTask.Application.DbLogs.Queries
{
    public class GetDbLogCommand : IRequest<DbLogDetailsDto>
    {
        public int LogId { get; set; }  
    }

    public class GetDbLogCommandHandler : IRequestHandler<GetDbLogCommand, DbLogDetailsDto>
    {
        private readonly IDbLogService _dbLogService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public GetDbLogCommandHandler(IDbLogService dbLogService, ITokenAuthenticationService authService,
            IMapper mapper)
        {
            _dbLogService = dbLogService;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<DbLogDetailsDto> Handle(GetDbLogCommand request, CancellationToken cancellationToken)
        {
            if (!_authService.UserRoleAdmin())
                throw new AuthenticationException();

            var logFromDb = await _dbLogService.GetLogAsync(request.LogId);

            return _mapper.Map<DbLogDetailsDto>(logFromDb);
        }
    }
}
