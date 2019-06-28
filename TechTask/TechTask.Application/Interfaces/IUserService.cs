using System;
using System.Threading.Tasks;
using TechTask.Application.Users.Commands;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetSingleUserAsync(Guid id);
        bool UserExistsCheck(LoginUserCommand user);
        void AddUser(User user);
        Task<int> SaveChangesAsync();
    }
}