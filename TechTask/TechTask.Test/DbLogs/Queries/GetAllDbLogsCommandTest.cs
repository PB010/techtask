using AutoMapper;
using NSubstitute;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TechTask.Application.DbLogs.Models;
using TechTask.Application.DbLogs.Queries;
using TechTask.Application.Interfaces;
using Xunit;

namespace TechTask.Test.DbLogs.Queries
{
    public class GetAllDbLogsCommandTest
    {
        private readonly IDbLogService _dbLogService;
        private readonly ITokenAuthenticationService _authService;
        private readonly IMapper _mapper;

        public GetAllDbLogsCommandTest()
        {
            _dbLogService = Substitute.For<IDbLogService>();
            _authService = Substitute.For<ITokenAuthenticationService>();
            _mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async Task GetAllLogsCommand_QueryTheDb_GetAllLogs()
        {
            var testHandler = new GetAllDbLogsHandler(_dbLogService, _authService, _mapper);
            var testCommand = new GetAllDbLogsCommand();
            var dto = new List<DbLogDetailsDto>();

            _authService.UserRoleAdmin().Returns(true);
            await testHandler.Handle(testCommand, new CancellationToken());

            var testLogsFromDb = await _dbLogService.Received(1).GetAllLogsAsync(testCommand.DbLogQueryParameters);
            _mapper.Map<List<DbLogDetailsDto>>(testLogsFromDb).Returns(dto);
        }
    }
}
