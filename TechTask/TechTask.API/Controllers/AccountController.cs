using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TechTask.Application.Users;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Users;

namespace TechTask.API.Controllers
{
    [Route("/api/account")]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(AppDbContext context) : base(context)
        {
        }

        [HttpPost]
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

            return CreatedAtRoute("SingleUser", new {id = userForDb.Id}, userForDb);
        }

        [HttpGet("{id}", Name = "SingleUser")]
        public IActionResult GetUser(Guid id)
        {
            return Ok(_context.Users.SingleOrDefault(u => u.Id == id));
        }
    }
}
