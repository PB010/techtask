using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTask.Application.Interfaces;
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

        public async Task<User> GetUserAsync(Guid id)
        {
            var user = await _context.Users.Include(u => u.Comments)
                .Include(u => u.Tasks)
                .Include(u => u.Log)
                .SingleOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.Comments)
                .Include(u => u.Log)
                .ToListAsync();
        }

        public bool UserExists(string email, string password)
        {
            var userToCheckByEmail = _context.Users
                .Any(u => u.Email == email && u.Password == password);

            return userToCheckByEmail;
        }

        public bool UserExists(Guid id)
        {
            var userToCheckById = _context.Users.Any(u => u.Id == id);

            return userToCheckById;
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
