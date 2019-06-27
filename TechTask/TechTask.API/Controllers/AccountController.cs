using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Users.Commands;
using TechTask.Application.Users.Models;

namespace TechTask.API.Controllers
{
    [Route("/api/account/")]
    [ApiController]
    public class AccountController : BaseController
    {
    

        [HttpPost("registration/")]
        public async Task<UserForLoginDto> Registration([FromBody] RegisterUserCommand user)
        {
            return await _mediator.Send(user);
        }

        [HttpPost("login/")]
        public IActionResult Login([FromBody] UserForLoginDto user)
        {
            var userFromDb = _context.Users.SingleOrDefault(u => u.Email == user.Email);
        
            //if role admin -> one type of jwt, else another type with more restrictions
            if (userFromDb == null)
                return NotFound("User was not found.");
        
            if (userFromDb.Password != user.Password)
                return BadRequest("Invalid password.");
        
            if (_service.IsAuthenticated(user, out var token))
                return Ok(token);
        
            return BadRequest("Invalid request.");
        }

        [HttpGet("{id}", Name = "SingleUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetUser(Guid id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        public AccountController(IMediator mediator, ITokenAuthenticationService service) : base(mediator, service)
        {}
    }
}
