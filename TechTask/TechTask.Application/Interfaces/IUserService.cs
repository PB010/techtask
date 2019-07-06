using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTask.Application.Users.Models;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        bool UserExists(string email, string password);
        bool UserExists(Guid id);
        Task<int> AddUser(User user);
        Task<int> UpdateUser(User user, UserForUpdateDto dto);
    }
}