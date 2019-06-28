using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
using TechTask.Application.Users.Commands;
using TechTask.Persistence.Context;
using TechTask.Persistence.Models.Users;

namespace TechTask.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User> GetSingleUserAsync(Guid id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public bool UserExistsCheck(LoginUserCommand user)
        {
            var userToCheck = _context.Users
                .Any(u => u.Email == user.Email && u.Password == user.Password);

            return userToCheck;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
