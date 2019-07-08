using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Application.DbLogs.Models;
using TechTask.Application.DbLogs.Queries;
using TechTask.Application.Filters.Validators.DbLogValidator;

namespace TechTask.API.Controllers
{
    [Route("/api/dbLogs/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class DbLogsController : BaseController
    {
        public DbLogsController(IMediator mediator) : base(mediator)
        {}

        [HttpGet]
        public async Task<IEnumerable<DbLogDetailsDto>> GetAllLogs([FromQuery] 
            DbLogQueryParameters query)
        {
            return await _mediator.Send(new GetAllDbLogsCommand {DbLogQueryParameters = query});
        }

        [HttpGet("{logId}")]
        [ServiceFilter(typeof(ValidateGetDbLog))]
        public async Task<DbLogDetailsDto> GetLog([FromRoute] int logId)
        {
            return await _mediator.Send(new GetDbLogCommand {LogId = logId});
        }
    }
}
