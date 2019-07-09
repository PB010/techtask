using AutoMapper;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.DbLogs.Models;
using TechTask.Application.DbLogs.Queries;
using TechTask.Application.Interfaces;
using Xunit;

namespace TechTask.Test.DbLogsTest.Queries
{
    public class GetDbLogCommandTest
    {
        private readonly IDbLogService _dbLogService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public GetDbLogCommandTest()
        {
            _dbLogService = Substitute.For<IDbLogService>();
            _authService = Substitute.For<ITokenAuthenticationService>();
            _mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task GetLogCommand_QueryTheDb_GetSpecificLog()
        {
            var dto = new DbLogDetailsDto();
            var testCommand = new GetDbLogCommand();
            var testHandler = new GetDbLogCommandHandler(_dbLogService, _authService, _mapper);

            _authService.UserRoleAdmin().Returns(true);
            await testHandler.Handle(testCommand, new CancellationToken());

            var testLogFromDb =  _dbLogService.Received(1).GetLogAsync(testCommand.LogId);
            _mapper.Map<DbLogDetailsDto>(testLogFromDb).Returns(dto);
        }
    }
}
