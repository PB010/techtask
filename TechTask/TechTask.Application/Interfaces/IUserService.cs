using System;
using System.Threading.Tasks;
using TechTask.Persistence.Models.Users;

namespace TechTask.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserAsync(Guid id);
        void RemoveUserFromTeamAsync(Guid id);
        bool UserExists(string email, string password);
        bool UserExists(Guid id);
        void AddUser(User user);
        Task<int> SaveChangesAsync();
    }
}