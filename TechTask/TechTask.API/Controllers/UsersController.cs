using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Application.Filters.Validator;
using TechTask.Application.Users.Commands;
using TechTask.Application.Users.Models;
using TechTask.Application.Users.Queries;

namespace TechTask.API.Controllers
{
    [Route("/api/users/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class UsersController : BaseController   
    {
        public UsersController(IMediator mediator) : base(mediator)
        {}

        [HttpPost("registration/")]
        [AllowAnonymous]
        public async Task<UserForLoginDto> Registration([FromBody] UserForRegistrationDto dto)
        {
            return await _mediator.Send(new RegisterUserCommand{UserForRegistrationDto = dto});
        }

        [HttpPost("login/")]
        [AllowAnonymous]
        
        public async Task<UserWithTokenDto> Login([FromBody] UserForLoginDto dto)
        {
            return await _mediator.Send(new LoginUserCommand{UserForLoginDto = dto});
        }

        [HttpGet("{userId}")]
        [ServiceFilter(typeof(ValidateRouteAttributes))]
        public async Task<UserDetailsDto> UserDetails([FromRoute] Guid userId)
        {
            return await _mediator.Send(new GetUserDataQuery{UserId = userId});
        }

        [HttpGet]
        public async Task<IEnumerable<UserDetailsDto>> GetAllUsers()
        {
            return await _mediator.Send(new GetAllUsersQuery());
        }
    }
}
