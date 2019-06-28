using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TechTask.Application.Users.Commands;
using TechTask.Application.Users.Models;
using TechTask.Application.Users.Queries;

namespace TechTask.API.Controllers
{
    [Route("/api/account/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(IMediator mediator) : base(mediator)
        {}

        [HttpPost("registration/")]
        [AllowAnonymous]
        public async Task<UserForLoginDto> Registration([FromBody] RegisterUserCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("login/")]
        [AllowAnonymous]
        public async Task<UserWithTokenDto> Login([FromBody] LoginUserCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<UserDetailsDto> UserDetails([FromRoute] Guid id)
        {
            return await _mediator.Send(new GetUserDataQuery{Id = id});
        }
        
    }
}
