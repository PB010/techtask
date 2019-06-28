using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechTask.Application.Users.Commands;
using TechTask.Application.Users.Models;

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

        //[HttpGet("{id}", Name = "SingleUser")]
        //public IActionResult GetUser(Guid id)
        //{
        //    var user = _user.GetSingleUserAsync(id);
        //
        //    if (user == null)
        //        return NotFound();
        //
        //    return Ok(user);
        //}

        
    }
}
