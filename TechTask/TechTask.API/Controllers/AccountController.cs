using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using TechTask.Application.Users;
using TechTask.Infrastructure.Services;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Users;

namespace TechTask.API.Controllers
{
    [Route("/api/account/")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITokenAuthenticationService _service;

        public AccountController(AppDbContext context, ITokenAuthenticationService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost("registration/")]
        public IActionResult Registration([FromBody] UserForCreationDto user)
        {
            var userForDb = new User
            {
                Id = new Guid(),
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Role = user.Role
            };

            _context.Users.Add(userForDb);
            _context.SaveChanges();

            return CreatedAtRoute("SingleUser",
                new {id = userForDb.Id},
                userForDb);
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
    }
}
